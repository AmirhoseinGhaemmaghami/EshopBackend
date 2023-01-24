using EshopBackend.Shared.Dtos.Paging;
using EshopBackend.Shared.Dtos.Product;
using EshopBackend.Shared.Dtos.ProductComment;
using EshopBackend.Shared.Entities.Store;
using EshopBackend.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Services
{
    internal class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository;
        private readonly IGenericRepository<ProductGallery> productGalleryRepository;
        private readonly IGenericRepository<ProductComment> productCommentRepository;

        public ProductService(IGenericRepository<Product> productRepository,
            IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository,
            IGenericRepository<ProductGallery> productGalleryRepository,
            IGenericRepository<ProductComment> ProductCommentRepository)
        {
            this.productRepository = productRepository;
            this.productSelectedCategoryRepository = productSelectedCategoryRepository;
            this.productGalleryRepository = productGalleryRepository;
            productCommentRepository = ProductCommentRepository;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var res = await this.productRepository.AddAsync(product);
            await productRepository.SaveChanges();
            return res;
        }


        public async Task<List<ProductGallery>> GetGallery(long productId)
        {
            return await this.productGalleryRepository
                .GetAllWithSpecAsync(pg => pg.ProductId == productId);
        }

        public async Task<Product> GetProductById(long productId)
        {
            return await this.productRepository.GetByIdAsync(productId);
        }

        public async Task<List<Product>> GetProducts(ProductsWithSpecInput inp)
        {
            List<long>? productIds = null;
            if (inp.CategoryIds != null && inp.CategoryIds.Length > 0)
                productIds = (await this.productSelectedCategoryRepository
                    .GetAllWithSpecAsync(pc => inp.CategoryIds.Contains(pc.ProductCategoryId)))
                    .Select(pc => pc.ProductId).ToList();


            Expression<Func<Product, bool>> whereExpr =
                (Product p) =>
                ((inp.StartPrice != null && inp.StartPrice < p.Price) || inp.StartPrice == null)
                &&
                ((inp.EndPrice != null && inp.EndPrice > p.Price) || inp.EndPrice == null)
                &&
                ((!String.IsNullOrEmpty(inp.Title) && p.Name.Contains(inp.Title)) ||
                String.IsNullOrEmpty(inp.Title))
                &&
                ((productIds != null && productIds.Contains(p.Id)) || productIds == null);

            Expression<Func<Product, Object>> orderByAsc = null;
            Expression<Func<Product, Object>> orderByDesc = null;
            if (!String.IsNullOrEmpty(inp.SortColumn))
            {
                if (!String.IsNullOrEmpty(inp.SortOrder) && inp.SortOrder.ToUpper() == "ASC")
                {
                    if (inp.SortColumn.ToUpper() == "PRICE")
                    {
                        orderByAsc = p => p.Price;
                    }
                }
                else if (!String.IsNullOrEmpty(inp.SortOrder) && inp.SortOrder.ToUpper() == "DESC")
                {
                    if (inp.SortColumn.ToUpper() == "PRICE")
                    {
                        orderByDesc = p => p.Price;
                    }
                }
            }

            return await productRepository.GetAllWithSpecAsync(
                whereExpr,
                OrderByAsc: orderByAsc,
                OrderByDesc: orderByDesc,
                paging: new PageInput()
                {
                    PageId = inp.PageId,
                    PageSize = inp.PageSize
                });
        }

        public async Task<int> GetProductsCount(ProductsWithSpecInput inp)
        {
            List<long>? productIds = null;
            if (inp.CategoryIds != null && inp.CategoryIds.Length > 0)
                productIds = (await this.productSelectedCategoryRepository
                .GetAllWithSpecAsync(pc => inp.CategoryIds.Contains(pc.ProductCategoryId)))
                .Select(pc => pc.ProductId).ToList();

            Expression<Func<Product, bool>> expr =
                (Product p) =>
                ((inp.StartPrice != null && inp.StartPrice < p.Price) || inp.StartPrice == null)
                &&
                ((inp.EndPrice != null && inp.EndPrice > p.Price) || inp.EndPrice == null)
                &&
                ((!String.IsNullOrEmpty(inp.Title) && p.Name.Contains(inp.Title)) ||
                String.IsNullOrEmpty(inp.Title))
                &&
                ((productIds != null && productIds.Contains(p.Id)) || productIds == null);

            return await productRepository.CountWithSpecAsync(expr);
        }

        public async Task<List<Product>> GetRelatedProducts(long productId)
        {
            var categoryIds = (await this.productSelectedCategoryRepository
                .GetAllWithSpecAsync(pc => pc.ProductId == productId))
                .Select(pc => pc.ProductCategoryId);

            var products = (await this.productSelectedCategoryRepository
                .GetAllWithSpecAsync(pc => categoryIds.Contains(pc.ProductCategoryId) && pc.ProductId != productId,
                Includes: pc => pc.Product))
                .Select(pc => pc.Product)
                .Distinct();

            return products.ToList();
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var res = await this.productRepository.UpdateAsync(product);
            await productRepository.SaveChanges();
            return res;
        }
    }
}
