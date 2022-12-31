using EshopBackend.Shared.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
    }
}
