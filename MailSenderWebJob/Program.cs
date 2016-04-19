using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mundialito.DAL.Games;
using System.Threading;
using Mundialito.DAL.Accounts;
using System.Net.Mail;
using System.Net;
using Mundialito.DAL.Bets;
using System.Configuration;

namespace MailSenderWebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        private static List<Game> openGames;

        
        static void Main()
        {
            Program.WriteLine("Mundialito Mail Sender app started running");
            Program.openGames = Program.GetOpenGames();
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            for (; Program.openGames.Count > 0; Program.openGames = Program.GetOpenGames())
            {
                long dueTime = Program.GetMilliscecondsToSleep(Program.openGames[0]);
                if (dueTime == -1)
                {
                    Program.WriteLine("Due time is " + dueTime + ", skipping as it already passed");
                    continue;
                }
                if (dueTime > 4294967294)
                {
                    Program.WriteLine("Due time is " + dueTime + ", going to sleep 1 day");
                    Thread.Sleep((int)TimeSpan.FromDays(1).TotalMilliseconds);
                    continue;
                }
                autoResetEvent.Reset();
                Timer timer = new Timer(new TimerCallback(Program.SendNotifications), (object)autoResetEvent, dueTime, Timeout.Infinite);
                autoResetEvent.WaitOne();
            }
            Program.WriteLine("Mundialito Mail Sender app finished - Press Enter");
            Console.ReadLine();
            
        }

        private static void SendNotifications(object stateInfo)
        {
            Program.WriteLine("***** Sending notifications started *****");
            AutoResetEvent autoResetEvent = (AutoResetEvent)stateInfo;
            List<Game> list = Enumerable.ToList<Game>(Enumerable.Where<Game>((IEnumerable<Game>)Program.openGames, (Func<Game, bool>)(game => game.Date == Program.openGames[0].Date)));
            Program.WriteLine(string.Format("Sending notifications on {0} games", (object)list.Count));
            foreach (Game game in list)
            {
                List<MundialitoUser> usersToNotify = Program.GetUsersToNotify(game);
                Program.WriteLine(string.Format("Sending notifications to {0} users on game {1}", (object)usersToNotify.Count, (object)game.GameId));
                foreach (MundialitoUser user in usersToNotify)
                {
                    Program.WriteLine(string.Format("Will send notification to {0}", (object)user.Email));
                    Program.SendNotification(user, game);
                }
            }
            autoResetEvent.Set();
            Program.WriteLine("***** End of Notification senidng *****");
        }

        private static void SendNotification(MundialitoUser user, Game game)
        {
            try
            {
                string sendGridUsername = ConfigurationManager.AppSettings["SendGridUserName"];
                string sendGridPassword = ConfigurationManager.AppSettings["SendGridPassword"];
                string linkAddress = ConfigurationManager.AppSettings["LinkAddress"];
                string fromAddress = ConfigurationManager.AppSettings["fromAddress"];
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(user.Email, user.FirstName + " " + user.LastName));
                message.From = new MailAddress(fromAddress, "EuroChamp");
                TimeSpan timeSpan = game.CloseTime - DateTime.UtcNow;
                message.Subject = string.Format("WARNING: The game between {0} and {1}, will be closed in {2} minutes and you havn't placed a bet yet", (object)game.HomeTeam.Name, (object)game.AwayTeam.Name, (object)(int)timeSpan.TotalMinutes);
                string content1 = string.Format("Please submit your bet as soon as possible");
                string content2 = "<p>Please submit your bet as soon as possible. <a href='" + linkAddress  + "'>Click here for the Bets Center</a></p>";
                message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(content1, (Encoding)null, "text/plain"));
                message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(content2, (Encoding)null, "text/html"));
                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                NetworkCredential networkCredential = new NetworkCredential(sendGridUsername, sendGridPassword);
                smtpClient.Credentials = (ICredentialsByHost)networkCredential;
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Program.WriteLine("Failed to send notification. Exception is " + ex.Message);
            }
        }

        private static List<MundialitoUser> GetUsersToNotify(Game game)
        {
            IEnumerable<MundialitoUser> source = new UsersRepository().AllUsers();
            Dictionary<string, Bet> gameBets = Enumerable.ToDictionary<Bet, string, Bet>(new BetsRepository().GetGameBets(game.GameId), (Func<Bet, string>)(bet => bet.UserId), (Func<Bet, Bet>)(bet => bet));
            return Enumerable.ToList<MundialitoUser>(Enumerable.Where<MundialitoUser>(source, (Func<MundialitoUser, bool>)(user => !gameBets.ContainsKey(user.Id))));
        }

        private static long GetMilliscecondsToSleep(Game openGame)
        {
            DateTime notificationTime = Program.GetNotificationTime(openGame);
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = notificationTime - now;
            Program.WriteLine(string.Format("Will scheduale notification for {0}, going to sleep {1} minutes", (object)notificationTime, (object)timeSpan.TotalMinutes));
            if (timeSpan.TotalMilliseconds >= 0.0)
                return (long)timeSpan.TotalMilliseconds;
            Program.WriteLine(string.Format("WARNING: Game {0} notification time passed, will not send notification", (object)notificationTime, (object)timeSpan.TotalMinutes));
            return -1;
        }

        private static DateTime GetNotificationTime(Game game)
        {
            DateTime dateTime = game.Date;
            dateTime = dateTime.Subtract(TimeSpan.FromMinutes(270.0));
            return dateTime.ToLocalTime();
        }

        private static List<Game> GetOpenGames()
        {
            List<Game> list1 = Enumerable.ToList<Game>((IEnumerable<Game>)new GamesRepository().Get(null, (Func<IQueryable<Game>, IOrderedQueryable<Game>>)null, ""));
            Program.WriteLine(string.Format("Got {0} games from database", (object)list1.Count));
            List<Game> list2 = Enumerable.ToList<Game>(Enumerable.Where<Game>((IEnumerable<Game>)Enumerable.OrderBy<Game, DateTime>((IEnumerable<Game>)list1, (Func<Game, DateTime>)(game => game.Date)), (Func<Game, bool>)(game => game.Date.Subtract(TimeSpan.FromMinutes(270.0)) > DateTime.UtcNow)));
            Program.WriteLine(string.Format("{0} games still can be notified", (object)list2.Count));
            return list2;
        }

        public static void WriteLine(string message)
        {
            Console.WriteLine("{0} - {1}", (object)DateTime.Now, (object)message);
        }
    }
}
