using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BrokeTogether.Application.Service;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Infrastructure.Data;
using BrokeTogether.Shared.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BrokeTogether.API.Extension
{
    public static class ServiceExtension
    {
        /// <summary>
        /// Configure to connect to databse 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigurePosgresContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("SupabaseConnection")));
        }

        public static void BindJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection("JwtSettings"))
            .Validate(s => !string.IsNullOrWhiteSpace(s.Issuer), "JwtSettings:Issuer is required.")
            .Validate(s => !string.IsNullOrWhiteSpace(s.Audience), "JwtSettings:Audience is required.")
            .Validate(s => !string.IsNullOrWhiteSpace(s.SigningKey) && s.SigningKey.Length >= 32,
                    "JwtSettings:SigningKey is required and should be at least 32 chars.")
            .Validate(s => s.AccessTokenMinutes > 0, "JwtSettings:AccessTokenMinutes must be > 0.")
            .ValidateOnStart();
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            // read strongly-typed settings
            var jwt = configuration.GetSection("JwtSettings").Get<JwtSettings>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true; // set false only for local HTTP
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwt!.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwt!.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwt!.SigningKey)),

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero // no grace window; tokens expire exactly on time
                    };
                }
            );
        }

        public static IServiceCollection CongigureServiceManager(this IServiceCollection services)
        {
            // Register concrete services first
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IHomeMemberService, HomeMemberService>();
            services.AddScoped<IShoppingListService, ShoppingListService>();
            services.AddScoped<IContributionService, ContributionService>();
            services.AddScoped<IContributionSplitService, ContributionSplitService>();
            services.AddScoped<IUserService, UserService>(); // can be a thin wrapper / placeholder

            // Aggregate
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}