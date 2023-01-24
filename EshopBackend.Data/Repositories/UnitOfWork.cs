using EshopBackend.Data.Context;
using EshopBackend.Shared.Entities;
using EshopBackend.Shared.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EshopContext context;
        private Hashtable repositories;

        public UnitOfWork(EshopContext context)
        {
            this.context = context;
        }

        public async Task<int> Complete()
        {
            return await this.context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.context?.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var name = typeof(T).Name;
            if (repositories == null)
            {
                repositories = new Hashtable();
            }
            if (!this.repositories.ContainsKey(name))
            {
                var repositorytype = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                        repositorytype.MakeGenericType(typeof(T)), this.context);

                this.repositories.Add(name, repositoryInstance);
            }
            return (IGenericRepository<T>)this.repositories[name];
        }
    }
}
