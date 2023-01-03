
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Jwt.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public string ExpireDate { get; set; }
    }
}
