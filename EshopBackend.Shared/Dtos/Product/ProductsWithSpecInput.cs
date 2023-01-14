using EshopBackend.Shared.Dtos.Paging;
using EshopBackend.Shared.Dtos.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Dtos.Product
{
    public class ProductsWithSpecInput: PagingSortingInput
    {
        public string? Title { get; set; }

        public decimal? StartPrice { get; set; }

        public decimal? EndPrice { get; set; }

        public decimal[]? CategoryIds { get; set; }
    }
}
