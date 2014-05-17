using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.DAL.GeneralBets
{
    public interface IGeneralBetsRepository
    {
        IEnumerable<GeneralBet> GetGeneralBets();

        GeneralBet GetGeneralBet(int betId);

        GeneralBet GetUserGeneralBet(String userId);

        bool IsGeneralBetExists(String userId);

        GeneralBet InsertGeneralBet(GeneralBet bet);

        void DeleteGeneralBet(int betId);

        void UpdateGeneralBet(GeneralBet bet);

        void Save(); 
    }
}
