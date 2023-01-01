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

        public SliderController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }

        // GET: api/<SliderController>
        [HttpGet]
        public async Task<ActionResult<List<Slider>>> Get()
        {
            return ApiResponse.Ok(await this.sliderService.GetActiveSliders());
        }

        // GET api/<SliderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slider>> Get(int id)
        {
            //return Ok(await this.sliderService.GetSliderById(id));
            return ApiResponse.BadRequest("sdfkj");
        }

        // POST api/<SliderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SliderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SliderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
