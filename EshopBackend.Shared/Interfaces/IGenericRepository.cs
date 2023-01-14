using EshopBackend.Shared.Dtos.Paging;
using EshopBackend.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetSingleWithSpecAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, Object>>? OrderByAsc = null,
            Expression<Func<T, Object>>? OrderByDesc = null,
            PageInput? paging = null,
            params Expression<Func<T, Object>>[] Includes);

        Task<List<T>> GetAllWithSpecAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, Object>>? OrderByAsc = null,
            Expression<Func<T, Object>>? OrderByDesc = null,
            PageInput? paging = null,
            params Expression<Func<T, Object>>[] Includes);

        Task<int> CountWithSpecAsync(Expression<Func<T, bool>>? filter = null);

        Task<T?> GetByIdAsync(long id);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<bool> DeleteByIdAsync(long id);

        Task<T> AddAsync(T entity);

        Task<bool> SaveChanges();
    }
}
