using EshopBackend.Shared.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Entities.Order
{
    public class OrderLine: BaseEntity
    {
        #region Properties

        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public int OrderQty { get; set; }

        public decimal Price { get; set; }

        #endregion

        #region Relations

        public Order Order { get; set; }

        public Product Product { get; set; }

        #endregion
    }
}
