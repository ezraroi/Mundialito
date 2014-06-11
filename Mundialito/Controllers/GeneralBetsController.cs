using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.ActionLogs;
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
        private const String ObjectType = "GeneralBet";
        private readonly IGeneralBetsRepository generalBetsRepository;
        private readonly ILoggedUserProvider userProivider;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IActionLogsRepository actionLogsRepository;

        public GeneralBetsController(IGeneralBetsRepository generalBetsRepository, ILoggedUserProvider userProivider, IDateTimeProvider dateTimeProvider, IActionLogsRepository actionLogsRepository)
        {
            if (generalBetsRepository == null)
                throw new ArgumentNullException("generalBetsRepository");
            if (userProivider == null)
                throw new ArgumentNullException("userProivider");
            if (dateTimeProvider == null)
                throw new ArgumentNullException("dateTimeProvider");
            if (actionLogsRepository == null)
                throw new ArgumentNullException("actionLogsRepository");

            this.dateTimeProvider = dateTimeProvider;
            this.generalBetsRepository = generalBetsRepository;
            this.userProivider = userProivider;
            this.actionLogsRepository = actionLogsRepository;
            
        }

        
        public IEnumerable<GeneralBetViewModel> GetAllGeneralBets()
        {
            if (!User.IsInRole("Admin") && dateTimeProvider.UTCNow < TournamentTimesUtils.GeneralBetsCloseTime)
            {
                throw new ArgumentException("General bets are still open for betting, you can't see other users bets yet");
            }
            return generalBetsRepository.GetGeneralBets().Select( bet => new GeneralBetViewModel(bet)).OrderBy( bet => bet.OwnerName);
        }

        [Route("has-bet/{username}")]
        [HttpGet]
        public Boolean HasBet(string username)
        {
            return generalBetsRepository.IsGeneralBetExists(username);
        }

        [Route("CanSubmitBets")]
        [HttpGet]
        public Boolean CanSubmitBets()
        {
            return dateTimeProvider.UTCNow  < TournamentTimesUtils.GeneralBetsCloseTime;
        }

        [Route("user/{username}")]
        [HttpGet]
        public GeneralBetViewModel GetUserGeneralBet(string username)
        {
            if (userProivider.UserName != username && dateTimeProvider.UTCNow < TournamentTimesUtils.GeneralBetsCloseTime)
                throw new ArgumentException("General bets are still open for betting, you can't see other users bets yet");

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
            generalBet.WinningTeamId = newBet.WinningTeamId;
            generalBet.GoldBootPlayer = newBet.GoldenBootPlayer;
            var res = generalBetsRepository.InsertGeneralBet(generalBet);
            Trace.TraceInformation("Posting new General Bet: {0}", generalBet);
            generalBetsRepository.Save();
            newBet.GeneralBetId = res.GeneralBetId;
            AddLog(ActionType.CREATE, String.Format("Posting new Generel Bet: {0}", res));
            return newBet;
        }

        [HttpPut]
        public UpdateGenralBetModel UpdateBet(int id, UpdateGenralBetModel bet)
        {
            if (dateTimeProvider.UTCNow > TournamentTimesUtils.GeneralBetsCloseTime)
                throw new ArgumentException("General bets are already closed for betting");
            var betToUpdate = new GeneralBet();
            betToUpdate.GeneralBetId = id;
            betToUpdate.WinningTeamId = bet.WinningTeamId;
            betToUpdate.GoldBootPlayer = bet.GoldenBootPlayer;
            betToUpdate.User = new MundialitoUser();
            betToUpdate.User.Id = userProivider.UserId;
            generalBetsRepository.UpdateGeneralBet(betToUpdate);
            generalBetsRepository.Save();
            Trace.TraceInformation("Updating General Bet: {0}", betToUpdate);
            AddLog(ActionType.UPDATE, String.Format("Updating new Generel Bet: {0}", betToUpdate));
            return bet;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{id}/resolve")]
        public void ResolveGeneralBet(int id, ResolveGeneralBetModel resolvedBet)
        {
            if (dateTimeProvider.UTCNow < TournamentTimesUtils.GeneralBetsResolveTime)
            {
                AddLog(ActionType.ERROR, "General bets are not closed for betting yet");
                throw new ArgumentException("General bets are not closed for betting yet");
            }

            var item = generalBetsRepository.GetGeneralBet(id);
            if (item == null)
            {
                AddLog(ActionType.ERROR, string.Format("General Bet '{0}' dosen't exits", id));
                throw new ObjectNotFoundException(string.Format("General Bet '{0}' dosen't exits", id));
            }

            Trace.TraceInformation("Resolved General Bet '{0}' with data: {1}", id, resolvedBet);
            item.Resolve(resolvedBet.PlayerIsRight, resolvedBet.TeamIsRight);
            generalBetsRepository.UpdateGeneralBet(item);
            generalBetsRepository.Save();
            AddLog(ActionType.UPDATE, String.Format("Resolved Generel Bet: {0}", item));
        }

        private void Validate()
        {
            if (generalBetsRepository.IsGeneralBetExists(userProivider.UserName))
            {
                AddLog(ActionType.ERROR, "You have already submitted your general bet, only update is permitted");
                throw new ArgumentException("You have already submitted your general bet, only update is permitted");
            }
            if (dateTimeProvider.UTCNow > TournamentTimesUtils.GeneralBetsCloseTime)
            {
                AddLog(ActionType.ERROR, "General bets are already closed for betting");
                throw new ArgumentException("General bets are already closed for betting");
            }
        }

        private void AddLog(ActionType actionType, String message)
        {
            try
            {
                actionLogsRepository.InsertLogAction(ActionLog.Create(actionType, ObjectType, message));
                actionLogsRepository.Save();
            }
            catch (Exception e)
            {
                Trace.TraceError("Exception during log. Exception: {0}", e.Message);
            }
        }
    }
}
