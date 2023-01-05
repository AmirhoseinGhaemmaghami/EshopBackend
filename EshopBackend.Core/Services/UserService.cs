using EshopBackend.Core.Jwt;
using EshopBackend.Core.Security;
using EshopBackend.Shared.Dtos.Account;
using EshopBackend.Shared.Entities.Account;
using EshopBackend.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> genericRepository;
        private readonly IHashUtility hashUtility;
        private readonly ITokenServcie tokenServcie;

        public UserService(IGenericRepository<User> genericRepository,
            IHashUtility hashUtility, ITokenServcie tokenServcie)
        {
            this.genericRepository = genericRepository;
            this.hashUtility = hashUtility;
            this.tokenServcie = tokenServcie;
        }

        public async Task<LoginResultDto> GetUserByEmail(string email)
        {
            var user = await this.genericRepository.GetEntitiesQuery()
                .SingleOrDefaultAsync(u => u.Email == email.ToLower().Trim());
            if (user == null)
                return null;

            if (!user.IsActivated)
            {
                return new LoginResultDto()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActivated = user.IsActivated
                };
            }
            var token = tokenServcie.createToken(user);
            return new LoginResultDto()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token.Token,
                TokenExpireDate = token.ExpireDate,
                IsActivated = user.IsActivated
            };
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                return (await this.genericRepository.GetAllAsync()).ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> IsDuplicateEmail(string email)
        {
            var emailTocheck = email.ToLower().Trim();


            return await this.genericRepository.GetEntitiesQuery()
                .AnyAsync(u => u.Email == emailTocheck);
        }

        public async Task<LoginResultDto> Login(LoginInputDto loginInputDto)
        {
            var emailToCheck = loginInputDto.Email;
            var passwordToCheck = loginInputDto.Password;
            var user = await genericRepository.GetEntitiesQuery()
                .FirstOrDefaultAsync(user => user.Email == loginInputDto.Email);
            if (user != null)
                if (hashUtility.VerifyHash(loginInputDto.Password, user.Password))
                {
                    if (!user.IsActivated)
                    {
                        return new LoginResultDto()
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            IsActivated = user.IsActivated
                        };
                    }
                    var token = tokenServcie.createToken(user);
                    return new LoginResultDto()
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Token = token.Token,
                        TokenExpireDate = token.ExpireDate,
                        IsActivated = user.IsActivated
                    };
                }

            return null;
        }

        public async Task<RegisterResultDto> Register(RegisterInputDto registerInputDto)
        {
            if (await IsDuplicateEmail(registerInputDto.Email))
            {
                return new RegisterResultDto() { DuplicateEmail = true, Success = false };
            }

            var user = new User()
            {
                Address = registerInputDto.Address.Sanitize(),
                Email = registerInputDto.Email.ToLower().Trim().Sanitize(),
                Password = hashUtility.GetHash(registerInputDto.Password.Sanitize()),
                FirstName = registerInputDto.FirstName.Sanitize(),
                IsActivated = false,
                LastName = registerInputDto.LastName.Sanitize(),
                EmailActivationCode = Guid.NewGuid().ToString()
            };
            await this.genericRepository.AddAsync(user);
            var res = await genericRepository.SaveChanges();
            return new RegisterResultDto() { Success = res, DuplicateEmail = false };
        }
    }
}
