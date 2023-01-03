using EshopBackend.Core.Jwt.Models;
using EshopBackend.Shared.Entities.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Jwt
{
    internal class JwtTokenServcie : ITokenServcie
    {
        private readonly IConfiguration configuration;

        public JwtTokenServcie(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public TokenModel createToken(User user)
        {
            var tokenKey = configuration["Token:Key"];
            var tokenKeyBytes = Encoding.UTF8.GetBytes(tokenKey);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var jwthandler = new JwtSecurityTokenHandler();

            var expireDate = DateTime.Now.AddDays(1);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = configuration["Token:Issuer"],
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKeyBytes)
                , SecurityAlgorithms.HmacSha512Signature)
            };

            var securityToken = jwthandler.CreateToken(tokenDescriptor);
            return new TokenModel()
            {
                Token = jwthandler.WriteToken(securityToken),
                ExpireDate = expireDate.ToString()
            };
        }
    }
}
