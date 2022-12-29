using EshopBackend.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Data.DI
{
    public static class RegisterToDi
    {
        public static IServiceCollection AddDataService(
            this IServiceCollection services, string ConnectionStr)
        {
            services.AddDbContext<EshopContext>(dbo => dbo.UseSqlServer(ConnectionStr));
            return services;
        }
    }
}
