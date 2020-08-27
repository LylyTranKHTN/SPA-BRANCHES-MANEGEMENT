using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        SpaContext Context { get; set; }
        void SaveChanges();
        Task<bool> SaveChangesAsync();
    }
}
