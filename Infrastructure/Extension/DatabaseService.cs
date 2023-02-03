using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extension
{
    public static class DatabaseService
    {
       
        public static IServiceCollection AddDatabaseService(this IServiceCollection service, IConfiguration configuration)
        {
            
            service.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)), ServiceLifetime.Transient);

       

            return service;
        }
    }
}
