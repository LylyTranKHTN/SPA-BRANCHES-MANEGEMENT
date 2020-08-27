using API.Model.Model;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Repository.Repository
{
    public interface IReviewServiceRepository : IGenericRepository<ReviewService>
    {

    }
    public class ReviewServiceRepository : GenericRepository<ReviewService>, IReviewServiceRepository
    {

    }
}
