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
using System.Diagnostics;

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Bets")]
    [Authorize]
    public class BetsController : ApiController
    {
        private readonly IBetsRepository betsRepository;
        private readonly IBetValidator betValidator;
        private readonly ILoggedUserProvider userProivider;
        private readonly IDateTimeProvider dateTimeProvider;

        public BetsController(IBetsRepository betsRepository, IBetValidator betValidator, ILoggedUserProvider userProivider, IDateTimeProvider dateTimeProvider)
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

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("dateTimeProvider");
            }
            this.dateTimeProvider = dateTimeProvider;
        }

        public IEnumerable<BetViewModel> GetAllBets()
        {
            return betsRepository.GetBets().Select(item => new BetViewModel(item, dateTimeProvider.UTCNow));
        }

        public BetViewModel GetBetById(int id)
        {
            var item = betsRepository.GetBet(id);

            if (item == null)
                throw new ObjectNotFoundException(string.Format("Bet with id '{0}' not found", id));

            return new BetViewModel(item, dateTimeProvider.UTCNow);
        }

        [HttpGet]
        [Route("user/{username}")]
        public IEnumerable<BetViewModel> GetUserBets(string username)
        {
            var bets = betsRepository.GetUserBets(username).ToList();
            return bets.Where(bet => !bet.IsOpenForBetting(dateTimeProvider.UTCNow) || userProivider.UserName == username).Select(bet => new BetViewModel(bet, dateTimeProvider.UTCNow));
        }

        [HttpPost]
        public NewBetModel PostBet(NewBetModel bet)
        {
            var newBet = new Bet();
            newBet.UserId = userProivider.UserId;
            newBet.GameId = bet.GameId;
            newBet.HomeScore = bet.HomeScore;
            newBet.AwayScore = bet.AwayScore;
            newBet.CardsMark = bet.CardsMark;
            newBet.CornersMark = bet.CornersMark;
            betValidator.ValidateNewBet(newBet);
            var res = betsRepository.InsertBet(newBet);
            Trace.TraceInformation("Posting new Bet: {0}", newBet);
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
            betToUpdate.CornersMark = bet.CornersMark;
            betToUpdate.CardsMark = bet.CardsMark;
            betToUpdate.GameId = bet.GameId;
            betToUpdate.UserId = userProivider.UserId;
            betValidator.ValidateUpdateBet(betToUpdate);
            betsRepository.UpdateBet(betToUpdate);
            betsRepository.Save();
            Trace.TraceInformation("Updating Bet: {0}", betToUpdate);
            return bet;
        }

        [HttpDelete]
        public void DeleteBet(int id)
        {
            Trace.TraceInformation("Deleting Bet {0}", id);
            betValidator.ValidateDeleteBet(id, userProivider.UserId);
            betsRepository.DeleteBet(id);
            betsRepository.Save();
        }
        
    }
}
