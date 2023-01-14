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

        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductResultDto>>> Get([FromQuery]ProductsWithSpecInput productsWithSpecInput)
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
