using Mundialito.DAL.Bets;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Bets")]
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

        public IEnumerable<Bet> GetAllBets()
        {
            return betsRepository.GetBets();
        }

        public Bet GetBetById(int id)
        {
            var item = betsRepository.GetBet(id);

            if (item == null)
                throw new ObjectNotFoundException(string.Format("Bet with id '{0}' not found", id));
            return item;
        }

        [HttpPost]
        public Bet PostBet(Bet bet)
        {
            // TODO - Validate that current logged user is creating the bet
            var res = betsRepository.InsertBet(bet);
            betsRepository.Save();
            return res;
        }

        [HttpPut]
        public Bet PutTeam(int id, Bet bet)
        {
            // TODO - Validate that current logged user is updating the bet
            betsRepository.UpdateBet(bet);
            betsRepository.Save();
            return bet;
        }

        [HttpDelete]
        public void DeleteBet(int id)
        {
            betsRepository.DeleteBet(id);
            betsRepository.Save();
        }
        
    }
}
