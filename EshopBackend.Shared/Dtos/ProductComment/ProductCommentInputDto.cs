using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Dtos.ProductComment
{
    public class ProductCommentInputDto
    {
        public long ProductId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }
    }
}
