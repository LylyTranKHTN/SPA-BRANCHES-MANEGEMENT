using API.Model.Enum;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Repository.Repository
{
    public interface ITherapistRepository : IGenericRepository<Staff>
    {

    }
    public class TherapistRepository : GenericRepository<Staff>, ITherapistRepository
    {
        /// <summary>
        /// Fillter a result with possition is therapist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Task<Staff> GetByIdAsync(object id)
        {
            var therapist = base.GetByIdAsync(id);
            if (therapist.Result.Possition == (int)Position.therapist)
            {
                return therapist;
            }
            return DbSet.FindAsync(0);
        }

    }
}
