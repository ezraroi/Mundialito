using Mundialito.DAL.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.Models
{
    public class UserModel
    {
        //private List<BetViewModel> bets = new List<BetViewModel>();
        
        public UserModel(MundialitoUser user)
        {
            Username = user.UserName;
            Name = String.Format("{0} {1}", user.FirstName, user.LastName);
            Points = 0;
            Id = user.Id;
            Email = user.Email;
        }

        public String Id { get; private set; }

        public String Email { get; set; }
        
        public bool IsAdmin { get; set; }

        public String Username { get; set; }

        public String Name { get; set; }

        public int Points { get; set; }

        public int Results { get; private set; }

        public int Marks { get; private set; }

        public int Corners { get; private set; }

        public int YellowCards { get; private set; }

        public void SetGeneralBet(GeneralBetViewModel generalBet)
        {
            if (generalBet.IsResolved)
            {
                Points += generalBet.Points;
            }
        }

        public void AddBet(BetViewModel bet)
        {
            if (bet.IsResolved)
            {
                Points += bet.Points;
                if (bet.ResultWin)
                {
                    Results++;
                }
                else if (bet.GameMarkWin)
                {
                    Marks++;
                }
                if (bet.CardsWin)
                    YellowCards++;
                if (bet.CornersWin)
                    Corners++;
            }
        }
    }
}