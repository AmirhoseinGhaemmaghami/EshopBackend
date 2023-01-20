using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Dtos.Product
{
    public class ProductWithDetailsResultDto
    {
        public ProductResultDto ProductResultDto { get; set; }

        public ProductGalleryResultDto ProductGalleryResultDto { get; set; }
    }
}
