﻿using EshopBackend.Shared.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Shared.Interfaces
{
    public interface IUserService: IDisposable
    {
        Task<List<User>> GetUsers();
    }
}