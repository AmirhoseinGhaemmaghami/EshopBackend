using EshopBackend.Data.Entities.Access;
using EshopBackend.Data.NewFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Data.Entities.Account
{
    public class User: BaseEntity
    {
        #region propeties

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public bool IsActivated { get; set; }

        public string EmailActivationCode { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        #endregion

        #region Relations

        public ICollection<UserRole> UserRoles { get; set; }

        #endregion
    }
}
