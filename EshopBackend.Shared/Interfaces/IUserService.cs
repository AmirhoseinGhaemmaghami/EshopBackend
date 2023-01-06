using EshopBackend.Shared.Dtos.Account;
using EshopBackend.Shared.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();

        Task<RegisterResultDto> Register(RegisterInputDto registerInputDto);

        Task<bool> IsDuplicateEmail(string email);

        Task<LoginResultDto> Login(LoginInputDto loginInputDto);

        Task<LoginResultDto> GetUserByEmail(string email);

        Task<bool> ConfirmEmail(int userId, string code);
    }
}
