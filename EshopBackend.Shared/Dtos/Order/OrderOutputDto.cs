using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Dtos.Order
{
    public class OrderOutputDto
    {
        public long OrderId { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool IsPaid { get; set; }

        public List<OrderLineOutputDto> OrderLines { get; set; }
    }
}
