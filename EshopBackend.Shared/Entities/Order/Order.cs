using EshopBackend.Shared.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Entities.Order
{
    public class Order : BaseEntity
    {
        #region MyRegion

        public DateTime PaymentDate { get; set; }

        public bool IsPaid { get; set; }

        public long UserId { get; set; }

        #endregion


        #region relations

        public User User { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }

        #endregion
    }
}
