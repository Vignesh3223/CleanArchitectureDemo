using CIT.HelpDesk.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIT.HelpDesk.Persistence.Extensions
{
    public static class DbContextExtension
    {
        public static void AddPersistenceLayer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext(configuration);
        }

        public static void AddDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("coreConnection");
            service.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(typeof(UserContext).Assembly.FullName)));
        }
    }
}

