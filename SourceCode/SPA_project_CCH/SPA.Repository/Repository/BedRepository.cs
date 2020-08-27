using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Repository.Repository
{
    public interface IBedRepository : IGenericRepository<Bed>
    {
        Task<IEnumerable<Bed>> GetBedIncludeRoomByOutletID(int OutletID);
    }
    public class BedRepository : GenericRepository<Bed>, IBedRepository
    {
        public virtual async Task<IEnumerable<Bed>> GetBedIncludeRoomByOutletID(int OutletID)
        {
            return await GetQueryable(x => x.Room1.Outlet == OutletID, y => y.OrderByDescending(z => z.Room), "Room1").ToListAsync();
        }
    }
}
