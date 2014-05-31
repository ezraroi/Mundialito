using System.Collections.Generic;
using System.Data.Entity;
using Mundialito.Models;
using System.Linq;
using System.Web.Http;

namespace Mundialito.DAL.Games
{
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
            return Get().Include(game => game.HomeTeam).Include(game => game.AwayTeam).Include(game => game.Stadium).SingleOrDefault(game => game.GameId == gameId);
        }

        public Game InsertGame(Game game)
        {
            return Insert((Game)game);
        }

        public void DeleteGame(int gameId)
        {
            Delete(gameId);
        }

        public void UpdateGame(Game game)
        {
            Update(game);
        }

        public IEnumerable<Game> GetStadiumGames(int id)
        {
            return GetGames().Where(game => game.StadiumId == id);
        }

        #endregion


        
    }
}