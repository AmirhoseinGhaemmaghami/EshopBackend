using AutoMapper;
using EshopBackend.Shared.Dtos.Product;
using EshopBackend.Shared.Dtos.Slider;
using EshopBackend.Shared.Entities.Site;
using EshopBackend.Shared.Entities.Store;
using System.Runtime.CompilerServices;

namespace EshopBackend.WebApi.Helpers.AutoMapperProfiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            this.CreateMap<Product, ProductResultDto>()
                .ForMember<string>(pr => pr.ImageUrl, option => option.MapFrom<ProductUrlResolver>());

            this.CreateMap<Slider, SliderResultDto>()
                .ForMember<string>(sdto => sdto.ImageUrl, options => options.MapFrom<SliderUrlResolver>());

            this.CreateMap<List<ProductGallery>, ProductGalleryResultDto>()
                .ForMember<List<string>>(dto => dto.ImageUrls, options => options.MapFrom<ProductGalleryurlResolver>());
        }
    }
}
