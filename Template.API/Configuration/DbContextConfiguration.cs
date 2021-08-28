using System;
using Template.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Template.API.Configuration
{
    public static class DbContextConfiguration
    {
        /// <summary>
        /// Centralizar as configurações do contexto do Entity Framework e do Identity Core
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLivrariaDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<LivrariaDbContext>(options =>
            {
                options.UseMySql(connString, ServerVersion.AutoDetect(connString))
                .LogTo(Console.WriteLine, LogLevel.Debug);
            });

            // Configurações Identity Core
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                // Para fins de desenvolvimento, desabilitado exigencia de senha forte.
                // e relacionados a email.

                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddRoles<IdentityRole>()
            .AddErrorDescriber<IdentityErrorDescriber>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<LivrariaDbContext>();

            return services;
        }

    }
}