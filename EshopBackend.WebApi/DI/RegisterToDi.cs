using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace EshopBackend.WebApi.DI
{
    public static class RegisterToDi
    {
        public static IServiceCollection AddApiServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(opt => opt.AddPolicy("any", policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            }));

            services.AddJWtAuthentication(configuration);
            services.AddAuthorization();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

        private static IServiceCollection AddJWtAuthentication(
            this IServiceCollection services, IConfiguration configuration)
        {
            var tokenKey = configuration["Token:Key"];
            var issuer = configuration["Token:Issuer"];
            var tokenKeyBytes = Encoding.UTF8.GetBytes(tokenKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //specify Issuer and Signin keys
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(tokenKeyBytes),
                        ValidIssuer = issuer,

                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,

                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}
