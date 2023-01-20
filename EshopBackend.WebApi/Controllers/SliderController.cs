using AutoMapper;
using EshopBackend.Shared.Dtos.Slider;
using EshopBackend.Shared.Entities.Site;
using EshopBackend.Shared.Interfaces;
using EshopBackend.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EshopBackend.WebApi.Controllers
{
    public class SliderController : BaseController
    {
        private readonly ISliderService sliderService;
        private readonly IMapper mapper;

        public SliderController(ISliderService sliderService, IMapper mapper)
        {
            this.sliderService = sliderService;
            this.mapper = mapper;
        }

        // GET: api/<SliderController>
        [HttpGet]
        public async Task<ActionResult<List<SliderResultDto>>> Get()
        {
            var sliders = await this.sliderService.GetActiveSliders();
            return ApiResponse.Ok(mapper.Map<List<SliderResultDto>>(sliders));
        }
    }
}
