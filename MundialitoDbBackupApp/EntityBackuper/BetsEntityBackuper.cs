using Mundialito.DAL.Bets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundialitoDbBackupApp.EntityBackuper
{
    public class BetsEntityBackuper : AbstractEntityBackuper<Bet>
    {
        public BetsEntityBackuper(string directoryName)
            : base(directoryName, "Bets")
        {

        }

        protected override List<string> GetFieldsToBackup()
        {
            return new List<string>() { "BetId", "UserId", "GameId", "HomeScore", "AwayScore", "CornersMark", "CardsMark", "Points", "CornersWin", "GameMarkWin", "ResultWin", "CardsWin" };
        }

        protected override List<Bet> GetAllEntites()
        {
            var repository = new BetsRepository();
            return repository.GetBets().ToList();
        }
    }
}
