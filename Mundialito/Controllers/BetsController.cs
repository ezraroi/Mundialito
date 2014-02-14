using Mundialito.DAL.Bets;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mundialito.Logic;
using Mundialito.DAL.Games;
using Mundialito.DAL.Accounts;

namespace Mundialito.Controllers
{
    // TODO - Add bet validator logic and test it
    [RoutePrefix("api/Bets")]
    [Authorize]
    public class BetsController : ApiController
    {
        private readonly IBetsRepository betsRepository;
        private readonly IBetValidator betValidator;
        private readonly IUserProvider userProvider;
        private readonly IUserProvider userProivider;

        public BetsController(IBetsRepository betsRepository, IBetValidator betValidator, IUserProvider userProivider)
        {
            if (betsRepository == null)
            {
                throw new ArgumentNullException("betsRepository"); 
            }
            this.betsRepository = betsRepository;

            if (betValidator == null)
            {
                throw new ArgumentNullException("betValidator"); 
            }
            this.betValidator = betValidator;

            if (userProivider == null)
            {
                throw new ArgumentNullException("userProivider");
            }
            this.userProivider = userProivider;
        }

        public IEnumerable<BetViewModel> GetAllBets()
        {
            return betsRepository.GetBets().Select(item => new BetViewModel(item));
        }

        public BetViewModel GetBetById(int id)
        {
            var item = betsRepository.GetBet(id);

            if (item == null)
                throw new ObjectNotFoundException(string.Format("Bet with id '{0}' not found", id));
            return new BetViewModel(item);
        }

        [HttpPost]
        public NewBetModel PostBet(NewBetModel bet)
        {
            var newBet = new Bet();
            newBet.User = new MundialitoUser();
            newBet.User.Id = userProivider.UserId;
            newBet.Game = new Game();
            newBet.Game.GameId = bet.GameId;
            newBet.HomeScore = bet.HomeScore;
            newBet.AwayScore = bet.AwayScore;
            betValidator.ValidateNewBet(newBet);
            var res = betsRepository.InsertBet(newBet);
            betsRepository.Save();
            bet.BetId = res.BetId;
            return bet;
        }

        [HttpPut]
        public UpdateBetModel UpdateBet(int id, UpdateBetModel bet)
        {
            var betToUpdate = new Bet();
            betToUpdate.BetId = id;
            betToUpdate.HomeScore = bet.HomeScore;
            betToUpdate.AwayScore = bet.AwayScore;
            betValidator.ValidateUpdateBet(betToUpdate);
            betsRepository.UpdateBet(betToUpdate);
            betsRepository.Save();
            return bet;
        }

        [HttpDelete]
        public void DeleteBet(int id)
        {
            betValidator.ValidateDeleteBet(id, userProivider.UserId);
            betsRepository.DeleteBet(id);
            betsRepository.Save();
        }
        
    }
}
