using EshopBackend.Shared.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Dtos.Order
{
    public class OrderLineOutputDto
    {
        public long ProductId { get; set; }

        public long OrderQty { get; set; }

        public decimal Price { get; set; }

        public ProductResultDto Product { get; set; }
    }
}
