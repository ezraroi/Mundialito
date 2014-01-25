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

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Bets")]
    [Authorize]
    public class BetsController : ApiController
    {
        private readonly IBetsRepository betsRepository;

        public BetsController(IBetsRepository betsRepository )
        {
            if (betsRepository == null)
            {
                throw new ArgumentNullException("betsRepository"); 
            }
            this.betsRepository = betsRepository;
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
            newBet.User.Id = User.Identity.GetUserId();
            newBet.Game.GameId = bet.GameId;
            newBet.HomeScore = bet.HomeScore;
            newBet.AwayScore = bet.AwayScore;
            var res = betsRepository.InsertBet(newBet);
            betsRepository.Save();
            bet.BetId = res.BetId;
            return bet;
        }

        [HttpPut]
        public UpdateBetModel UpdateBet(int id, UpdateBetModel bet)
        {
            AuthorizeAction(id);

            var betToUpdate = new Bet();
            betToUpdate.BetId = id;
            betToUpdate.HomeScore = bet.HomeScore;
            betToUpdate.AwayScore = bet.AwayScore;
            betsRepository.UpdateBet(betToUpdate);
            betsRepository.Save();
            return bet;
        }

        [HttpDelete]
        public void DeleteBet(int id)
        {
            AuthorizeAction(id);
            betsRepository.DeleteBet(id);
            betsRepository.Save();
        }

        private void AuthorizeAction(int id)
        {
            var originalBet = betsRepository.GetBet(id);
            if (originalBet == null)
                throw new ObjectNotFoundException(string.Format("Bet with id '{0}' not found", id));

            if (originalBet.User.Id != User.Identity.GetUserId())
                throw new UnauthorizedAccessException("You can not add bet of another user");
        }
    }
}
