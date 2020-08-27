using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Repository.Repository
{
    public interface IReviewOutletRepository : IGenericRepository<ReviewOutlet>
    {
        
    }
    public class ReviewOutletRepository : GenericRepository<ReviewOutlet>, IReviewOutletRepository
    {
        
    }
}
