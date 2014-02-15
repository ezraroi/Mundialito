using System;
using System.Collections.Generic;
using Mundialito.Models;

namespace Mundialito.DAL.Games
{
    public interface IGamesRepository : IDisposable
    {
        IEnumerable<Game> GetGames();
        Game GetGame(int gameId);
        Game InsertGame(Game game);
        void DeleteGame(int gameId);
        void UpdateGame(Game game);
        void Save(); 

    }
    
}