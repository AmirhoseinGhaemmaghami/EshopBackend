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

        Task<LoginResultDto> GetUserById(long id);

        Task<bool> ConfirmEmail(int userId, string code);

        Task<EditUserResultDto> UpdateUser(long userId, string firstname, string lastname, string address);
    }
}
