using System.Collections.Generic;
using System.Data.Entity;
using Mundialito.Models;
using System.Linq;
using System.Web.Http;

namespace Mundialito.DAL.Games
{
    [AllowAnonymous]
    public class GamesRepository : GenericRepository<Game>,IGamesRepository
    {

        #region Implementation of IGamesRepository

        public GamesRepository() : base(new MundialitoContext())
        {
        }

        public IEnumerable<IGame> GetGames()
        {
            return Get().Include(game => game.HomeTeam).Include(game => game.AwayTeam).Include(game => game.Stadium).OrderBy(game => game.Date);
        }

        public IGame GetGame(int gameId)
        {
            return Get().Include(game => game.HomeTeam).Include(game => game.AwayTeam).Include(game => game.Stadium).SingleOrDefault(game => game.GameId == gameId);
        }

        public IGame InsertGame(IGame game)
        {
            // TODO - Check the status of the attched items, force that the items are not new
            Context.Teams.Attach(game.AwayTeam);
            Context.Teams.Attach(game.HomeTeam);
            Context.Stadiums.Attach(game.Stadium);
            return (IGame)Insert((Game)game);
        }

        public void DeleteGame(int gameId)
        {
            Delete(gameId);
        }

        public void UpdateGame(IGame game)
        {
            Update((Game)game);
        }

        #endregion

       
    }
}