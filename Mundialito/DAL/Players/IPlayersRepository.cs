using System;
using System.Collections.Generic;

namespace Mundialito.DAL.Players
{
    public interface IPlayersRepository : IDisposable
    {

        IEnumerable<Player> GetPlayers();

        Player GetPlayer(int playerId);

        Player InsertPlayer(Player player);

        void DeletePlayer(int playerId);

        void Save();

    }
}