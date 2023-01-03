using EshopBackend.Core.Jwt.Models;
using EshopBackend.Shared.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Jwt
{
    public interface ITokenServcie
    {
        TokenModel createToken(User user);
    }
}
