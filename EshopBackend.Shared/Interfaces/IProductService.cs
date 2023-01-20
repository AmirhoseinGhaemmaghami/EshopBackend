using EshopBackend.Shared.Dtos.Product;
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

        Task<List<Product>> GetProducts(ProductsWithSpecInput productsWithSpecInput);

        Task<int> GetProductsCount(ProductsWithSpecInput productsWithSpecInput);

        Task<Product> GetProductById(long productId);

        Task<List<ProductGallery>> GetGallery(long productId);

        Task<List<Product>> GetRelatedProducts(long id);
    }
}
