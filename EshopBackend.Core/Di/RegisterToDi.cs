using EshopBackend.Core.Jwt;
using EshopBackend.Core.Security;
using EshopBackend.Core.Services;
using EshopBackend.Core.Services.Utilities;
using EshopBackend.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Di
{
    public static class RegisterToDi
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IProductService, ProductService>();

            MD5 md5Hash = MD5.Create();
            services.AddSingleton(typeof(HashAlgorithm), md5Hash);
            services.AddScoped<IHashUtility, HashUtility>();

            services.AddScoped<ITokenServcie, JwtTokenServcie>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductCommentService, ProductCommentService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
