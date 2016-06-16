using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mundialito.DAL.Games;
using Mundialito.DAL.Accounts;
using System.Net.Mail;
using System.Net;
using Mundialito.DAL.Bets;
using System.Configuration;
using Microsoft.Azure.WebJobs;
using System.IO;

namespace MailSenderWebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    public class Program
    {
        private static List<Game> openGames;


        static void Main()
        {
            var config = new JobHostConfiguration();
            config.UseTimers();
            JobHost host = new JobHost(config);
            host.RunAndBlock();
        }

        public static void RunJob([TimerTrigger("00:30:00")] TimerInfo timerInfo, TextWriter log)
        {
            log.WriteLine("*********************************** Mundialito Mail Sender app started running***********************************");
            Program.openGames = Program.GetOpenGames(log);
            for (int i = 0; i < Program.openGames.Count; i++)
            {
                var minutes = Program.openGames[i].Date.ToLocalTime().Subtract(DateTime.Now.ToLocalTime()).TotalMinutes;
                log.WriteLine("Game " + Program.openGames[i].GameId + " Minutes is " + minutes);
                if (minutes < 120 && minutes > 35)
                {
                    log.WriteLine("Found game that will start @ " + Program.openGames[i].Date.ToLocalTime());
                    log.WriteLine(minutes + " Minutes until start time");
                    SendNotifications(Program.openGames[i], log);
                }
            }
            log.WriteLine("***********************************  Bye bye ***********************************");
        }

        private static void SendNotifications(Game game, TextWriter log)
        {
            log.WriteLine("***** Sending notifications started *****");
            log.WriteLine("Current Time: " + DateTime.Now.ToLocalTime());
            log.WriteLine(string.Format("Sending notifications on game {0} ", game.GameId));
            List<MundialitoUser> usersToNotify = Program.GetUsersToNotify(game);
            log.WriteLine(string.Format("Sending notifications to {0} users on game {1}", usersToNotify.Count, game.GameId));
            foreach (MundialitoUser user in usersToNotify)
            {
                log.WriteLine(string.Format("Will send notification to {0}", user.Email));
                Program.SendNotification(user, game, log);
            }
            log.WriteLine("***** End of Notification sending *****");
        }

        private static void SendNotification(MundialitoUser user, Game game, TextWriter log)
        {
            try
            {
                string sendGridUsername = ConfigurationManager.AppSettings["SendGridUserName"];
                string sendGridPassword = ConfigurationManager.AppSettings["SendGridPassword"];
                string linkAddress = ConfigurationManager.AppSettings["LinkAddress"];
                string fromAddress = ConfigurationManager.AppSettings["fromAddress"];
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(user.Email, user.FirstName + " " + user.LastName));
                message.From = new MailAddress(fromAddress, ConfigurationManager.AppSettings["ApplicationName"]);
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
                log.WriteLine("Failed to send notification. Exception is " + ex.Message);
                if (ex.InnerException != null)
                {
                    log.WriteLine("Innber excpetion: " + ex.InnerException.Message);
                }
                
            }
        }

        private static List<MundialitoUser> GetUsersToNotify(Game game)
        {
            IEnumerable<MundialitoUser> source = new UsersRepository().AllUsers();
            Dictionary<string, Bet> gameBets = Enumerable.ToDictionary<Bet, string, Bet>(new BetsRepository().GetGameBets(game.GameId), (Func<Bet, string>)(bet => bet.UserId), (Func<Bet, Bet>)(bet => bet));
            return Enumerable.ToList<MundialitoUser>(Enumerable.Where<MundialitoUser>(source, (Func<MundialitoUser, bool>)(user => !gameBets.ContainsKey(user.Id))));
        }
        
        private static List<Game> GetOpenGames(TextWriter log)
        {
            List<Game> list1 = Enumerable.ToList<Game>(new GamesRepository().Get(null, null, ""));
            log.WriteLine(string.Format("Got {0} games from database", list1.Count));
            List<Game> list2 = Enumerable.ToList<Game>(Enumerable.Where<Game>((IEnumerable<Game>)Enumerable.OrderBy<Game, DateTime>((IEnumerable<Game>)list1, (Func<Game, DateTime>)(game => game.Date)), (Func<Game, bool>)(game => game.IsOpen())));
            log.WriteLine(string.Format("{0} games still can be notified", list2.Count));
            return list2;
        }
      
    }
}
