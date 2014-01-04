using System;
using System.Collections.Generic;
using Mundialito.Models;

namespace Mundialito.DAL.Stadiums
{
    public interface IStadiumsRepository : IDisposable
    {
        IEnumerable<Stadium> GetStadiums();
        Stadium GetStadium(int stadiumId);
        Stadium InsertStadium(Stadium stadium);
        void DeleteStadium(int stadiumId);
        void UpdateStadium(Stadium stadium);
        void Save();
    }
}