using Mundialito.DAL.Accounts;
using Mundialito.DAL.GeneralBets;
using Mundialito.DAL.Teams;
using Mundialito.Logic;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mundialito.Controllers
{
    [RoutePrefix("api/GeneralBets")]
    [Authorize]
    public class GeneralBetsController : ApiController
    {
        private IGeneralBetsRepository generalBetsRepository;
        private ILoggedUserProvider userProivider;
        private IDateTimeProvider dateTimeProvider;

        public GeneralBetsController(IGeneralBetsRepository generalBetsRepository, ILoggedUserProvider userProivider, IDateTimeProvider dateTimeProvider)
        {
            if (generalBetsRepository == null)
            {
                throw new ArgumentNullException("generalBetsRepository");
            }
            if (userProivider == null)
            {
                throw new ArgumentNullException("userProivider");
            }
            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("dateTimeProvider");
            }

            this.dateTimeProvider = dateTimeProvider;
            this.generalBetsRepository = generalBetsRepository;
            this.userProivider = userProivider;
        }

        public IEnumerable<GeneralBetViewModel> GetAllGeneralBets()
        {
            return generalBetsRepository.GetGeneralBets().Select( bet => new GeneralBetViewModel(bet));
        }


        [Route("has-bet/{username}")]
        [HttpGet]
        public Boolean HasBet(string username)
        {
            return generalBetsRepository.IsGeneralBetExists(username);
        }

        [Route("user/{username}")]
        [HttpGet]
        public GeneralBetViewModel GetUserGeneralBet(string username)
        {
            var item = generalBetsRepository.GetUserGeneralBet(username);

            if (item == null)
                throw new ObjectNotFoundException(string.Format("User '{0}' dosen't have a general bet yet", username));

            return new GeneralBetViewModel(item);
        }

        public GeneralBetViewModel GetGeneralBetById(int id)
        {
            var item = generalBetsRepository.GetGeneralBet(id);

            if (item == null)
                throw new ObjectNotFoundException(string.Format("General Bet with id '{0}' not found", id));

            return new GeneralBetViewModel(item);
        }

        [HttpPost]
        public NewGeneralBetModel PostBet(NewGeneralBetModel newBet)
        {
            Validate();
            var generalBet = new GeneralBet();
            generalBet.User = new MundialitoUser();
            generalBet.User.Id = userProivider.UserId;
            generalBet.WinningTeam = new Team();
            generalBet.WinningTeam.TeamId = newBet.TeamId;
            generalBet.GoldBootPlayer = newBet.Player;
            var res = generalBetsRepository.InsertGeneralBet(generalBet);
            Trace.TraceInformation("Posting new General Bet: {0}", generalBet);
            generalBetsRepository.Save();
            newBet.GenrealBetId = res.GeneralBetId;
            return newBet;
        }

        

        [HttpPut]
        public UpdateGenralBetModel UpdateBet(int id, UpdateGenralBetModel bet)
        {
            if (dateTimeProvider.UTCNow > Constants.GeneralBetsCloseTime)
                throw new ArgumentException("General bets are already closed for betting");
            var betToUpdate = new GeneralBet();
            betToUpdate.GeneralBetId = id;
            betToUpdate.WinningTeam = new Team();
            betToUpdate.WinningTeam.TeamId = bet.TeamId;
            betToUpdate.GoldBootPlayer = bet.Player;
            generalBetsRepository.UpdateGeneralBet(betToUpdate);
            generalBetsRepository.Save();
            Trace.TraceInformation("Updating General Bet: {0}", betToUpdate);
            return bet;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{id}/resolve")]
        public void ResolveGeneralBet(int id, ResolveGeneralBetModel resolvedBet)
        {
            Trace.TraceInformation("Resolved General Bet '{0}' with data: {1}", id, resolvedBet);
            if (dateTimeProvider.UTCNow < Constants.GeneralBetsCloseTime)
                throw new ArgumentException("General bets are not closed for betting yet");

            var item = generalBetsRepository.GetGeneralBet(id);
            if (item == null)
                throw new ObjectNotFoundException(string.Format("General Bet '{0}' dosen't exits", id));
            item.Resolve(resolvedBet.PlayerIsRight, resolvedBet.TeamIsRight);
            generalBetsRepository.UpdateGeneralBet(item);
            generalBetsRepository.Save();
        }

        private void Validate()
        {
            if (generalBetsRepository.IsGeneralBetExists(userProivider.UserName))
                throw new ArgumentException("You have already sibmitted yoyr genral bet, only update is permitted");
            if (dateTimeProvider.UTCNow > Constants.GeneralBetsCloseTime)
                throw new ArgumentException("General bets are already closed for betting");
        }
    }
}
