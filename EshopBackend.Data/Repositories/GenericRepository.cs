using EshopBackend.Data.Context;
using EshopBackend.Shared.Dtos.Paging;
using EshopBackend.Shared.Entities;
using EshopBackend.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly EshopContext eshopContext;
        private DbSet<T> dbset;

        public GenericRepository(EshopContext eshopContext)
        {
            this.eshopContext = eshopContext;
            this.dbset = this.eshopContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = entity.CreateDate;
            await this.dbset.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            entity.Deleted = true;
            await this.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(long id)
        {
            var entity = await this.GetByIdAsync(id);
            return await this.DeleteAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.dbset.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await this.dbset.FindAsync(id);
        }

        public Task<T> UpdateAsync(T entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            var res = dbset.Update(entity);
            return Task.FromResult(entity);
        }

        public async Task<List<T>> GetAllWithSpecAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, Object>>? OrderByAsc = null,
            Expression<Func<T, Object>>? OrderByDesc = null,
            PageInput? paging = null,
            params Expression<Func<T, Object>>[] Includes)
        {
            var query = Evaluate(this.dbset, filter, OrderByAsc, OrderByDesc, paging, Includes);
            return await query.ToListAsync();
        }

        public async Task<T?> GetSingleWithSpecAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, Object>>? OrderByAsc = null,
            Expression<Func<T, Object>>? OrderByDesc = null,
            PageInput? paging = null,
            params Expression<Func<T, Object>>[] Includes)
        {
            var query = Evaluate(this.dbset, filter,OrderByAsc, OrderByDesc, paging, Includes);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> CountWithSpecAsync(Expression<Func<T, bool>>? filter = null)
        {
            var query = Evaluate(this.dbset, filter, null, null, null);
            return await query.CountAsync();
        }

        private IQueryable<T> Evaluate(IQueryable<T> query,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, Object>>? OrderByAsc = null,
            Expression<Func<T, Object>>? OrderByDesc = null,
            PageInput? paging = null,
            params Expression<Func<T, Object>>[] Includes)
        {
            if (filter != null)
                query = query.Where(filter);
            if (OrderByAsc != null)
                query = query.OrderBy(OrderByAsc);
            if (OrderByDesc != null)
                query = query.OrderBy(OrderByDesc);
            if (paging != null)
                query = query.Skip((paging.PageId - 1) * paging.PageSize).Take(paging.PageSize);
            if (Includes.Any())
            {
                query = Includes.Aggregate(query, (q, inc) => q.Include(inc));
            }

            return query;
        }

        public async Task<bool> SaveChanges()
        {
            return await this.eshopContext.SaveChangesAsync() > 0;
        }
    }
}
