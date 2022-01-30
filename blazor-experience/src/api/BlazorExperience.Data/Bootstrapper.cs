using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BlazorExperience.Data
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    .ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.Debug)))
                    .UseSqlServer(configuration.GetConnectionString("BlazorExpDb"),
                        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                            .MigrationsAssembly("BlazorExperience.Data"))
            );

            services.AddScoped<IDataContext, DataContext>(serviceProvider => serviceProvider.GetService<DataContext>());

            return services;
        }
    }
}