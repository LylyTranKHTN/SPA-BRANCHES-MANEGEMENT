using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPA.Domain.Models;

namespace SPA.Repository.Repository
{ 
    public interface IOutlet_StaffRepository : IGenericRepository<Outlet_Staff>
    {
    }
    public class  Outlet_StaffRepository:GenericRepository<Outlet_Staff>, IOutlet_StaffRepository
    {

    }
}
