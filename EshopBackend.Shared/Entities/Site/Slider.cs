using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Entities.Site
{
    public class Slider: BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Body { get; set; }

        [MaxLength(200)]
        public string Url { get; set; }

        [Required]
        [MaxLength(200)]
        public string ImageUrl { get; set; }
    }
}
