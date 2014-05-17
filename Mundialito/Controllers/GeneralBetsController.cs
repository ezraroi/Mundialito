using Mundialito.DAL.GeneralBets;
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

        public GeneralBetsController(IGeneralBetsRepository generalBetsRepository)
        {
            if (generalBetsRepository == null)
            {
                throw new ArgumentNullException("generalBetsRepository");
            }
            this.generalBetsRepository = generalBetsRepository;
        }

        public IEnumerable<GeneralBet> GetAllGeneralBets()
        {
            return generalBetsRepository.GetGeneralBets();
        }

        [Route("user/{username}")]
        public GeneralBet GetUserGeneralBet(string username)
        {
            var item = generalBetsRepository.GetUserGeneralBet(username);

            if (item == null)
                throw new ObjectNotFoundException(string.Format("User '{0}' dosen't have a general bet yet", username));

            return item;
        }

        public GeneralBet GetBetById(int id)
        {
            var item = generalBetsRepository.GetGeneralBet(id);

            if (item == null)
                throw new ObjectNotFoundException(string.Format("General Bet with id '{0}' not found", id));
            return item;
        }

        [HttpPost]
        public GeneralBet PostBet(GeneralBet bet)
        {
            return null;
        }

        [HttpPut]
        public GeneralBet UpdateBet(int id, GeneralBet bet)
        {
            return null;
        }

        [HttpDelete]
        public void DeleteBet(int id)
        {
            Trace.TraceInformation("Deleting General Bet {0}", id);
            // TODO - Validate that the user can delete this bet
            generalBetsRepository.DeleteGeneralBet(id);
            generalBetsRepository.Save();
        }
    }
}
