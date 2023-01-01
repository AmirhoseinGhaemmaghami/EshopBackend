using EshopBackend.Data.Context;
using EshopBackend.Shared.Entities;
using EshopBackend.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<T> GetEntitiesQuery()
        {
            return dbset.AsQueryable();
        }

        public Task<T> UpdateAsync(T entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            var res = dbset.Update(entity);
            return Task.FromResult(entity);
        }

        public async Task<bool> SaveChanges()
        {
            return await this.eshopContext.SaveChangesAsync() > 0;
        }
    }
}
