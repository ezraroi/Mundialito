using System;
using System.Collections.Generic;
using Mundialito.Models;

namespace Mundialito.DAL.Games
{
    public interface IGamesRepository : IDisposable
    {
        IEnumerable<IGame> GetGames();
        IGame GetGame(int gameId);
        IGame InsertGame(IGame game);
        void DeleteGame(int gameId);
        void UpdateGame(IGame game);
        void Save(); 

    }
    
}