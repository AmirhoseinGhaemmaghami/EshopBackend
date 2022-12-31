using EshopBackend.Shared.Entities;
using EshopBackend.Shared.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Entities.Access
{
    public class UserRole : BaseEntity
    {
        #region properties

        public long UserId { get; set; }

        public long RoleId { get; set; }

        #endregion

        #region Relations

        public User User { get; set; }

        public Role Role { get; set; }

        #endregion
    }
}
