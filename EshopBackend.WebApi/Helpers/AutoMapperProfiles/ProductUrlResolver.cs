using AutoMapper;
using EshopBackend.Shared.Dtos.Product;
using EshopBackend.Shared.Entities.Store;

namespace EshopBackend.WebApi.Helpers.AutoMapperProfiles
{
    public class ProductUrlResolver : IValueResolver<Product, ProductResultDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            var baseUrl = this.configuration["BaseUrl"];
            return baseUrl + "/images/products/origin/" + source.ImageUrl;
        }
    }
}
