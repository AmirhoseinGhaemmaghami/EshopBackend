using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Entities.Store
{
    public class ProductVisit: BaseEntity
    {
        #region properties

        public long ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string VisitorIp { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }

        #endregion
    }
}
