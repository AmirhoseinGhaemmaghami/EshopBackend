using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Dtos.ProductComment
{
    public class ProductCommentResultDto
    {
        public long UserId { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserEmail { get; set; }

        public long ProductId { get; set; }

        public string Comment { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
