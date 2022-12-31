using EshopBackend.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Interfaces
{
    public interface IGenericRepository<T> : IDisposable where T : BaseEntity
    {
        IQueryable<T> GetEntitiesQuery();

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(long id);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<bool> DeleteByIdAsync(long id);

        Task<T> AddAsync(T entity);

        Task<bool> SaveChanges();
    }
}
