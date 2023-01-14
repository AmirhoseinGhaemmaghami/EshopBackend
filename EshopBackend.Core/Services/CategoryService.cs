using EshopBackend.Shared.Entities.Store;
using EshopBackend.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Services
{
    internal class CategoryService: ICategoryService
    {
        private readonly IGenericRepository<ProductCategory> genericRepository;

        public CategoryService(IGenericRepository<ProductCategory> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task<List<ProductCategory>> GetAllCategories()
        {
            return await this.genericRepository.GetAllAsync() as List<ProductCategory>;
        }
    }
}
