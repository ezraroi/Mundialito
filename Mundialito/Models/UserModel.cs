using Mundialito.DAL.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.Models
{
    public class UserModel
    {
        private List<BetViewModel> bets = new List<BetViewModel>();

        public UserModel(MundialitoUser user)
        {
            Username = user.UserName;
            Name = String.Format("{0} {1}", user.FirstName, user.LastName);
            Points = 0;
        }

        public String Username { get; set; }

        public String Name { get; set; }

        public int Points { get; set; }

        public List<BetViewModel> Bets { get { return bets; } }

        public void AddBet(BetViewModel bet)
        {
            bets.Add(bet);
            if (bet.IsResolved)
                Points += int.Parse(bet.Points);
        }
        
    }
}