using EshopBackend.Shared.Entities;
using EshopBackend.Shared.Entities.Access;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Entities.Account
{
    public class User : BaseEntity
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

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

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
