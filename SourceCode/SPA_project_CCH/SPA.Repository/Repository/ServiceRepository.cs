using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Repository.Repository
{
    public interface IServiceRepository : IGenericRepository<Service>
    {

    }
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {

    }


}
