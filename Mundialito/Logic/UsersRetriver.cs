using Microsoft.AspNet.Identity;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.Bets;
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
        private IBetsRepository betsRepository;
        private IUsersRepository usersRepository;

        public UsersRetriver(IBetsRepository betsRepository, IUsersRepository usersRepository)
        {
            this.betsRepository = betsRepository;
            this.usersRepository = usersRepository;
        }

        public UserModel GetUser(String username, bool isLogged)
        {
            var user = usersRepository.GetUser(username);
            if (user == null)
            {
                throw new ObjectNotFoundException(string.Format("No such user '{0}'", username));
            }
            var userModel = new UserModel(user);
            betsRepository.GetUserBets(user.Id).Where(bet => isLogged || !bet.IsOpenForBetting).ToList().ForEach(bet => userModel.AddBet(new BetViewModel(bet)));
            return userModel;
        }

        public List<UserModel> GetAllUsers()
        {
            var users = usersRepository.AllUsers().ToDictionary(user => user, user => new UserModel(user));
            betsRepository.GetBets().Where(bet => !bet.IsOpenForBetting).ToList().ForEach(bet => users[bet.User].AddBet(new BetViewModel(bet)));
            return users.Values.ToList();
        }
    }
}