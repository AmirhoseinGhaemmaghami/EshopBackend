using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Entities.Store
{
    public class ProductCategory: BaseEntity
    {
        #region propeties
        [Required]
        [MaxLength(100)]
        public string name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public long? ParentCategoryId { get; set; }

        #endregion

        #region relations

        [ForeignKey(nameof(ParentCategoryId))]
        public ProductCategory? ParentCategory { get; set; }

        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        #endregion
    }
}
