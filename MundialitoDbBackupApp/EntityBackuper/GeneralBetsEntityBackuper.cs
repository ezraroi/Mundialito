using Mundialito.DAL.Accounts;
using Mundialito.DAL.GeneralBets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundialitoDbBackupApp.EntityBackuper
{
    public class GeneralBetsEntityBackuper : AbstractEntityBackuper<GeneralBet>
    {

        public GeneralBetsEntityBackuper(string directoryName)
            : base(directoryName, "Genenral Bets")
        {

        }

        protected override List<string> GetFieldsToBackup()
        {
            return new List<string>() { "GeneralBetId", "WinningTeamId", "GoldBootPlayer", "TeamPoints", "PlayerPoints", "User" };
        }

        protected override List<GeneralBet> GetAllEntites()
        {
            var repository = new GeneralBetsRepository();
            return repository.GetGeneralBets().ToList();
        }

        protected override object ProcessValue(object obj, string propName)
        {
            if (obj is MundialitoUser)
                return ((MundialitoUser)obj).UserName;
            return obj;
        }
    }
}
