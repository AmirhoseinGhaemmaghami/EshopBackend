using EshopBackend.Shared.Entities.Site;
using EshopBackend.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Services
{
    public class SliderService : ISliderService
    {
        private readonly IGenericRepository<Slider> repository;

        public SliderService(IGenericRepository<Slider> repository)
        {
            this.repository = repository;
        }

        public async Task<Slider> AddSlider(Slider slider)
        {
            var res = await this.repository.AddAsync(slider);
            await repository.SaveChanges();
            return res;
        }

        public async Task<bool> DeleteSlider(long id)
        {
            var res = await repository.DeleteByIdAsync(id);
            await repository.SaveChanges();
            return res;
        }

        public async Task<List<Slider>> GetActiveSliders()
        {
            return await repository.GetEntitiesQuery()
                .Where(s => !s.Deleted)
                .ToListAsync();
        }

        public async Task<List<Slider>> GetAllSliders()
        {
            return (await repository.GetAllAsync())
                .ToList();
        }

        public async Task<Slider> GetSliderById(long id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<Slider> UpdateSlider(Slider slider)
        {
            var res = await repository.UpdateAsync(slider);
            await repository.SaveChanges();
            return res;
        }
    }
}
