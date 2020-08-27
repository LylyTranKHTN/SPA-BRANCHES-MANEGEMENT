using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPA.Domain.Models;

namespace SPA.Repository.Repository
{
    public interface IService_BedRepository:IGenericRepository<Service_Bed>
    {
    }
    class Service_BedRepository:GenericRepository<Service_Bed>,IService_BedRepository
    {
    }
}
