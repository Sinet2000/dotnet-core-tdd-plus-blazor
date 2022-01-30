using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorExperience.Data;
using BlazorExperience.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorExperience.Services
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDataServices(configuration);

            services.AddScoped<IBookService, BookService>();

            return services;
        }
    }
}
