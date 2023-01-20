using AutoMapper;
using EshopBackend.Shared.Dtos.Product;
using EshopBackend.Shared.Entities.Store;

namespace EshopBackend.WebApi.Helpers.AutoMapperProfiles
{
    public class ProductGalleryurlResolver : IValueResolver<List<ProductGallery>, ProductGalleryResultDto, List<string>>
    {
        private readonly IConfiguration configuration;

        public ProductGalleryurlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public List<string> Resolve(List<ProductGallery> source, ProductGalleryResultDto destination, List<string> destMember, ResolutionContext context)
        {
            var baseUrl = this.configuration["BaseUrl"];
            return source?.Select(p => baseUrl + "/images/products/origin/" + p.ImageUrl).ToList() ?? new List<string>();
        }
    }
}
