using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.Bets;
using Mundialito.DAL.GeneralBets;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;

namespace Mundialito.Logic
{
    public class UsersRetriver : IUsersRetriver
    {
        private readonly IBetsRepository betsRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IGeneralBetsRepository generalBetsRepository;
        private readonly IDateTimeProvider dateTimeProvider;

        public UsersRetriver(IBetsRepository betsRepository, IGeneralBetsRepository generalBetsRepository, IUsersRepository usersRepository, IDateTimeProvider dateTimeProvider)
        {
            this.betsRepository = betsRepository;
            this.usersRepository = usersRepository;
            this.generalBetsRepository = generalBetsRepository;
            this.dateTimeProvider = dateTimeProvider;
        }

        public UserModel GetUser(String username, bool isLogged)
        {
            var user = usersRepository.GetUser(username);
            if (user == null)
            {
                throw new ObjectNotFoundException(string.Format("No such user '{0}'", username));
            }
            var userModel = new UserModel(user);
            betsRepository.GetUserBets(user.UserName).Where(bet => isLogged || !bet.IsOpenForBetting(dateTimeProvider.UTCNow)).ToList().ForEach(bet => userModel.AddBet(new BetViewModel(bet, dateTimeProvider.UTCNow)));
            var generalBet = generalBetsRepository.GetUserGeneralBet(username);
            if (generalBet != null)
            {
                userModel.SetGeneralBet(new GeneralBetViewModel(generalBet));
            }
            return userModel;
        }

        public List<UserModel> GetAllUsers()
        {
            var users = usersRepository.AllUsers().ToDictionary(user => user.Id, user => new UserModel(user));
            var allBets = betsRepository.GetBets();
            allBets.Where(bet => users.ContainsKey(bet.User.Id)).Where(bet => !bet.IsOpenForBetting(dateTimeProvider.UTCNow)).ToList().ForEach(bet => users[bet.User.Id].AddBet(new BetViewModel(bet, dateTimeProvider.UTCNow)));
            allBets.Where(bet => users.ContainsKey(bet.User.Id)).Where(bet => !bet.IsOpenForBetting(dateTimeProvider.UTCNow)).Where(bet => bet.Game.Date < dateTimeProvider.UTCNow.Subtract(TimeSpan.FromDays(1))).ToList().ForEach(bet => users[bet.User.Id].YesterdayPoints += bet.Points.HasValue ? bet.Points.Value : 0);
            generalBetsRepository.GetGeneralBets().ToList().ForEach(generalBet => 
            {
                users[generalBet.User.Id].SetGeneralBet(new GeneralBetViewModel(generalBet));
            });
            return users.Values.ToList();
        }
    }
}