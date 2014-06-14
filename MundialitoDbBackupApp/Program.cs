using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.Games;
using MundialitoDbBackupApp.EntityBackuper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MundialitoDbBackupApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Mundialito Database backup app started runnig");
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            WriteLine("Starting first manual backup");
            BackupDatabase(autoEvent);
            autoEvent.WaitOne();
            WriteLine("Finished first manual backup");
            autoEvent.Reset();
            var openGames = GetOpenGames();
            while(openGames.Count > 0)
            {
                autoEvent.Reset();
                var timer = new Timer(BackupDatabase, autoEvent, GetMilliscecondsToSleep(openGames[0]), System.Threading.Timeout.Infinite);
                autoEvent.WaitOne();
                openGames = GetOpenGames();
            }
            Console.ReadLine();
        }

        private static List<Game> GetOpenGames()
        {
            var gamesRepository = new GamesRepository();
            var allGames = gamesRepository.Get().ToList();
            WriteLine(string.Format("Got {0} games from database", allGames.Count));
            var openGames = allGames.OrderBy(game => game.Date).Where(game => game.Date.Subtract(TimeSpan.FromMinutes(30)) > DateTime.UtcNow).ToList();
            WriteLine(string.Format("{0} games are still open", openGames.Count));
            return openGames;
        }

        private static int GetMilliscecondsToSleep(Game openGame)
        {
            var backupTime = GetGameBackupTime(openGame);
            var now = DateTime.Now;
            var span = (backupTime - now);
            WriteLine(string.Format("Will scheduale backup for {0}, going to sleep {1} minutes", backupTime, span.TotalMinutes));
            return (int) span.TotalMilliseconds;
        }

        private static DateTime GetGameBackupTime(Game game)
        {
            return game.Date.Subtract(TimeSpan.FromMinutes(20)).ToLocalTime();
        }

        public static void WriteLine(string message)
        {
            Console.WriteLine("{0} - {1}", DateTime.Now, message);
        }

        private static void BackupDatabase(Object stateInfo)
        {
            WriteLine("***** Start Backing up database *****");
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;

            var directory = Directory.CreateDirectory(DateTime.Now.ToString("dd-MM-yy-HH-mm"));

            WriteLine("Created direcotry " + directory.Name);

            var usersBackuper = new UsersEntityBackuper(directory.FullName);
            usersBackuper.Backup();

            var actionLogsEntityBackuper = new ActionLogsEntityBackuper(directory.FullName);
            actionLogsEntityBackuper.Backup();

            var stadiumsEntityBackuper = new StadiumsEntityBackuper(directory.FullName);
            stadiumsEntityBackuper.Backup();

            var teamsEntityBackuper = new TeamsEntityBackuper(directory.FullName);
            teamsEntityBackuper.Backup();

            var generalBetsEntityBackuper = new GeneralBetsEntityBackuper(directory.FullName);
            generalBetsEntityBackuper.Backup();

            var gamesBackuper = new GamesBackuper(directory.FullName);
            gamesBackuper.Backup();

            var betsEntityBackuper = new BetsEntityBackuper(directory.FullName);
            betsEntityBackuper.Backup();

            autoEvent.Set();
            WriteLine("***** End of Backing up database *****");
        }
        
    }
}
