using EshopBackend.Shared.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Entities.Store
{
    public class ProductComment: BaseEntity
    {
        public long UserId { get; set; }

        public long ProductId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }

        #region Relations

        public User User { get; set; }

        public Product Product { get; set; }

        #endregion
    }
}
