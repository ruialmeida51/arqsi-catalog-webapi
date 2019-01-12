using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Server.Helper;
using Server.Repository.Base;
using Server.Repository.Wrapper;
using Server.Service.AuthenticationService;
using IAuthenticationService = Server.Service.IAuthenticationService;

namespace Server.Extensions
{
    public static class ServiceExtensions
    {
        /**
        * Sets up the parameters for CORS policy.
        * At the moment, it has no restrictions.
        */
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        /**
        * Sets up the database.
        */
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration config)
        {
            if (Environment.GetEnvironmentVariable("ENV") == "PRODUCTION")
            {
                services.AddDbContext<RepositoryContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("PersistenceContext"))); 
            }
            else
            {
                services.AddDbContext<RepositoryContext>(options =>
                    options.UseSqlite("Filename=./database.db")); 
            }
        }

        /**
        * Repository wrapper used to instantiate the repositories. 
        */
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        /**
         * Sets up the application for JWT Authentication.
         */
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration config)
        {
            // Configure secret key stored in appsettings.json
            var appSettingsSection = config.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // Configure the JWT Token.
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Add authentication service dependency injection
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}