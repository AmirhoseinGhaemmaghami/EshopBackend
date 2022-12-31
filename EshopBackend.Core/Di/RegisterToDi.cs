using EshopBackend.Core.Services;
using EshopBackend.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Di
{
    public static class RegisterToDi
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            return services.AddScoped<IUserService, UserService>();
        }
    }
}
