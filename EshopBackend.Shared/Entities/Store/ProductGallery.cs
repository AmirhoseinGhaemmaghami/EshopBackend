using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Entities.Store
{
    public class ProductGallery: BaseEntity
    {
        #region properties

        [Required]
        public long ProductId { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }

        #endregion

        #region relation

        public Product Product { get; set; }

        #endregion
    }
}
