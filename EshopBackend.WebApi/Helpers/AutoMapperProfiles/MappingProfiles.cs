using AutoMapper;
using EshopBackend.Shared.Dtos.Product;
using EshopBackend.Shared.Entities.Store;
using System.Runtime.CompilerServices;

namespace EshopBackend.WebApi.Helpers.AutoMapperProfiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            this.CreateMap<Product, ProductResultDto>();
        }
    }
}
