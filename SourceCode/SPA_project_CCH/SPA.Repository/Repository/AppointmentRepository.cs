using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace SPA.Repository.Repository
{
    public interface IAppoitmentRepository : IGenericRepository<Appointment>
    {
    }
    public class AppoitmentRepository : GenericRepository<Appointment>, IAppoitmentRepository
    {
       
    }


}
