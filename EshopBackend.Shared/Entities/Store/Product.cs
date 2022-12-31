using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Entities.Store
{
    public class Product: BaseEntity
    {
        #region properties

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(500)]
        public string ShortDescription { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }

        [Required]
        public bool Existed { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool SpecialProduct { get; set; }

        #endregion

        #region relations

        public ICollection<ProductGallery> productGalleries { get; set; }

        public ICollection<ProductVisit> ProductVisits { get; set; }

        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        #endregion
    }
}
