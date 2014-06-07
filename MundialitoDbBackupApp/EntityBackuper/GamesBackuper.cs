using Mundialito.DAL.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundialitoDbBackupApp
{
    public class GamesBackuper : AbstractEntityBackuper<Game>
    {
        public GamesBackuper(string directoryName)
            : base(directoryName, "Games")
        {

        }

        protected override List<string> GetFieldsToBackup()
        {
            return new List<string>() { "GameId", "HomeTeamId", "AwayTeamId", "Date", "HomeScore", "AwayScore", "CornersMark", "CardsMark", "StadiumId"};
        }

        protected override List<Game> GetAllEntites()
        {
            var repository = new GamesRepository();
            return repository.GetGames().ToList();
        }
    }
}
