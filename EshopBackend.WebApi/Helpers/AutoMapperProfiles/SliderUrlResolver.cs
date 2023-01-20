using AutoMapper;
using EshopBackend.Shared.Dtos.Slider;
using EshopBackend.Shared.Entities.Site;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EshopBackend.WebApi.Helpers.AutoMapperProfiles
{
    public class SliderUrlResolver : IValueResolver<Slider, SliderResultDto, string>
    {
        private readonly IConfiguration configuration;

        public SliderUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Slider source, SliderResultDto destination, string destMember, ResolutionContext context)
        {
            var baseUrl = configuration["BaseUrl"];
            return baseUrl + "/images/sliders/origin/" + source.ImageUrl;
        }
    }
}
