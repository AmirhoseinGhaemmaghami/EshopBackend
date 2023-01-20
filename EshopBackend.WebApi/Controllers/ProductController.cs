using AutoMapper;
using EshopBackend.Shared.Dtos.Paging;
using EshopBackend.Shared.Dtos.Product;
using EshopBackend.Shared.Entities.Store;
using EshopBackend.Shared.Interfaces;
using EshopBackend.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EshopBackend.WebApi.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public ProductController(IProductService productService, IMapper mapper, IConfiguration configuration)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductResultDto>>> Get([FromQuery] ProductsWithSpecInput productsWithSpecInput)
        {
            var products = await this.productService.GetProducts(productsWithSpecInput);
            var count = await this.productService.GetProductsCount(productsWithSpecInput);

            return ApiResponse.Ok(new Pagination<ProductResultDto>()
            {
                Data = mapper.Map<List<ProductResultDto>>(products),
                Count = count,
                PageId = productsWithSpecInput.PageId,
                PageSize = productsWithSpecInput.PageSize
            });
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDto>> Get(long id)
        {
            var res = await this.productService.GetProductById(id);
            if (res != null)
                return ApiResponse.Ok(mapper.Map<ProductResultDto>(res));
            return ApiResponse.NotFound("Product Not Found");
        }

        // GET api/<ProductController>/5
        [HttpGet("ProductWithAllDetails/{id}")]
        public async Task<ActionResult<List<ProductWithDetailsResultDto>>> GetProductWithDetails(long id)
        {
            var resProduct = await this.productService.GetProductById(id);
            var resGalley = await this.productService.GetGallery(id);

            if (resProduct != null)
            {
                var ret = new ProductWithDetailsResultDto()
                {
                    ProductResultDto = mapper.Map<ProductResultDto>(resProduct),
                    ProductGalleryResultDto = mapper.Map<ProductGalleryResultDto>(resGalley)
                };
                return ApiResponse.Ok(mapper.Map<ProductWithDetailsResultDto>(ret));
            }
            return ApiResponse.NotFound("Product Not Found");
        }

        [HttpGet("Related/{id}")]
        public async Task<ActionResult<List<ProductResultDto>>> GetRelatedProducts(long id)
        {
            List<Product> ret = await this.productService.GetRelatedProducts(id);

            return ApiResponse.Ok(mapper.Map<List<ProductResultDto>>(ret));
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
