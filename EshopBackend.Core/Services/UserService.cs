using AngleSharp.Dom;
using AngleSharp.Io;
using EshopBackend.Core.Jwt;
using EshopBackend.Core.Security;
using EshopBackend.Shared.Dtos.Account;
using EshopBackend.Shared.Entities.Account;
using EshopBackend.Shared.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EshopBackend.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> genericRepository;
        private readonly IHashUtility hashUtility;
        private readonly ITokenServcie tokenServcie;
        private readonly IEmailConfirmationService emailService;

        public UserService(IGenericRepository<User> genericRepository,
            IHashUtility hashUtility, ITokenServcie tokenServcie, IEmailConfirmationService emailService)
        {
            this.genericRepository = genericRepository;
            this.hashUtility = hashUtility;
            this.tokenServcie = tokenServcie;
            this.emailService = emailService;
        }

        public async Task<bool> ConfirmEmail(int userId, string code)
        {
            var uuidCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var user = await this.genericRepository.GetByIdAsync(userId);
            if(user?.EmailActivationCode == uuidCode)
            {
                user.IsActivated = true;
                user.EmailActivationCode = Guid.NewGuid().ToString();
                await this.genericRepository.UpdateAsync(user);
                await this.genericRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<LoginResultDto> GetUserById(long userId)
        {
            var user = await this.genericRepository.GetByIdAsync(userId);
            if (user == null)
                return null;

            if (!user.IsActivated)
            {
                return new LoginResultDto()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActivated = user.IsActivated,
                    Address = user.Address
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
                IsActivated = user.IsActivated,
                Address= user.Address
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


            return await this.genericRepository.GetSingleWithSpecAsync(u => u.Email == emailTocheck) != null;
        }

        public async Task<LoginResultDto> Login(LoginInputDto loginInputDto)
        {
            var emailToCheck = loginInputDto.Email;
            var passwordToCheck = loginInputDto.Password;
            var user = await genericRepository.GetSingleWithSpecAsync(user => user.Email == loginInputDto.Email);
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

            await this.emailService.SendVefrificationEmail(user);

            return new RegisterResultDto() { Success = res, DuplicateEmail = false };
        }

        public async Task<EditUserResultDto> UpdateUser(long userId, string firstname, string lastname, string address)
        {
            var user = await this.genericRepository.GetByIdAsync(userId);
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Address = address;
            await this.genericRepository.UpdateAsync(user);
            await this.genericRepository.SaveChanges();
            return new EditUserResultDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address
            };
        }
    }
}
