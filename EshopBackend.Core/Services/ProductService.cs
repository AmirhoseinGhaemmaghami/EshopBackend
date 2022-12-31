using EshopBackend.Shared.Entities.Store;
using EshopBackend.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Services
{
    internal class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> repository;

        public ProductService(IGenericRepository<Product> repository)
        {
            this.repository = repository;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var res = await this.repository.AddAsync(product);
            await repository.SaveChanges();
            return res;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var res = await this.repository.UpdateAsync(product);
            await repository.SaveChanges();
            return res;
        }
    }
}
