using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
namespace Mundialito.DAL.Games
{
    public interface IGame
    {
        int? AwayScore { get;  }

        Team AwayTeam { get; }

        DateTime Date { get;  }

        int GameId { get;  }

        int? HomeScore { get;  }

        Team HomeTeam { get;  }

        bool IsOpen { get; }

        bool IsPendingUpdate { get; }

        string Mark { get; }

        Stadium Stadium { get;  }
    }
}
