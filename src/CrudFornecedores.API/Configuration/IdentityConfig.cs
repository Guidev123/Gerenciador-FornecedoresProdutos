using CrudFornecedores.API.Data;
using CrudFornecedores.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CrudFornecedores.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(
                  options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<UserDbContext>()
                    .AddErrorDescriber<IdentityCustomMessages>()
                    .AddDefaultTokenProviders();

            return services;
        }


        public static void AddJwtConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // AUTENTICAR COM BASE NO TOKEN
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // VERIFICAR COM BASE NO TOKEN
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true; // VAI REQUERER HTTPS, EVITAR MAN IN THE MIDDLE
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    // VALIDACOES 
                    ValidateIssuerSigningKey = true, 
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidoEm,
                    ValidIssuer = appSettings.Emissor
                };
            });
        }
    }
}
