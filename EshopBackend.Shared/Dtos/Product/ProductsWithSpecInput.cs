using EshopBackend.Shared.Dtos.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Dtos.Product
{
    public class ProductsWithSpecInput: PageInput
    {
        public string? Title { get; set; }

        public decimal? StartPrice { get; set; }

        public decimal? EndPrice { get; set; }

        public decimal[]? CategoryIds { get; set; }
    }
}
