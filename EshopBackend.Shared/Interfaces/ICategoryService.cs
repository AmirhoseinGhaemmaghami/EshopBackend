using EshopBackend.Shared.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Interfaces
{
    public interface ICategoryService
    {
        Task<List<ProductCategory>> GetAllCategories();
    }
}
