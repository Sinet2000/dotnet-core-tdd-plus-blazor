using BlazorExperience.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExperience.Api.Tests
{
    public class ApiApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = builder.Build();
            host.Start();

            // Get the service provider
            var serviceProvider = host.Services;

            // Create a scope to obtain a reference to the database
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DataContext>();

                var logger = scopedServices.GetRequiredService<ILogger<ApiApplicationFactory<TStartup>>>();

                // Ensure the database is created
                db.Database.EnsureCreated();
            }

            return host;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                string inMemoryCollectionName = Guid.NewGuid().ToString();

                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase(inMemoryCollectionName);
                });

                services.AddScoped<>
            });
        }
    }
}
