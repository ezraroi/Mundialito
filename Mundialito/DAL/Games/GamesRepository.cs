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

        public IEnumerable<Game> GetGames()
        {
            return Get().Include(game => game.HomeTeam).Include(game => game.AwayTeam).Include(game => game.Stadium).OrderBy(game => game.Date);
        }

        public Game GetGame(int gameId)
        {
            return GetByID(gameId);
        }

        public Game InsertGame(Game game)
        {
            Context.Teams.Attach(game.AwayTeam);
            Context.Teams.Attach(game.HomeTeam);
            Context.Stadium.Attach(game.Stadium);
            return Insert(game);
        }

        public void DeleteGame(int gameId)
        {
            Delete(gameId);
        }

        public void UpdateGame(Game game)
        {
            Update(game);
        }

        #endregion

       
    }
}