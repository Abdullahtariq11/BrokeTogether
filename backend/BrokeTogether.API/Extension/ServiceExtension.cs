using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

    }
}