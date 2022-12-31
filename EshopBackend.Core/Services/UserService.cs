using EshopBackend.Shared.Entities.Account;
using EshopBackend.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> genericRepository;

        public UserService(IGenericRepository<User> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task<List<User>> GetUsers()
        {
            return (await this.genericRepository.GetAllAsync()).ToList();
        }

        public void Dispose()
        {
            genericRepository?.Dispose();
        }
    }
}
