using EshopBackend.Shared.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllSliders();
        Task<List<Slider>> GetActiveSliders();
        Task<Slider> AddSlider(Slider slider);
        Task<Slider> UpdateSlider(Slider slider);
        Task<Slider> GetSliderById(long id);
        Task<bool> DeleteSlider(long id);
    }
}
