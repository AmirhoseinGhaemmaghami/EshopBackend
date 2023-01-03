using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Dtos.Account
{
    public class LoginResultDto
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }

        public string TokenExpireDate { get; set; }

        public bool IsActivated { get; set; }
    }
}
