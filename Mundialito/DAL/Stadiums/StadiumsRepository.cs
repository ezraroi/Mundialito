using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Mundialito.Models;


namespace Mundialito.DAL.Stadiums
{
    public class StadiumsRepository : GenericRepository<Stadium>, IStadiumsRepository
    {

        public StadiumsRepository()
            : base(new MundialitoContext())
        {
        }

        #region Implementation of IStadiumsRepository

        public IEnumerable<Stadium> GetStadiums()
        {
            return Get().OrderBy(stadium => stadium.Name);
        }

        public Stadium GetStadium(int stadiumId)
        {
            return Context.Stadium.Where(stadium => stadium.StadiumId == stadiumId).Include(stadium => stadium.Games).SingleOrDefault();
        }

        public Stadium InsertStadium(Stadium stadium)
        {
            return Insert(stadium);
        }

        public void DeleteStadium(int stadiumId)
        {
            Delete(stadiumId);
        }

        public void UpdateStadium(Stadium stadium)
        {
            Update(stadium);
        }

        #endregion
    }
}