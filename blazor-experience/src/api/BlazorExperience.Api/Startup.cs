using Microsoft.OpenApi.Models;
using System.Reflection;
using BlazorExperience.Api.Configuration;
using BlazorExperience.Services;

namespace BlazorExperience.Api
{
    public class Startup
    {
        private const string API_CODE = "blazorexp-api";
        private const string CORS_POLICY = "BlazorExpCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appConfig = new ApiSettings();
            var authConfig = new AuthSettings();
            var swaggerConfig = new SwaggerGenOptions();

            Configuration.Bind(ApiSettings.CONFIG_KEY, appConfig);
            Configuration.Bind(AuthSettings.CONFIG_KEY, authConfig);
            Configuration.Bind(SwaggerGenOptions.CONFIG_KEY, swaggerConfig);

            services.Configure<ApiSettings>(Configuration.GetSection(ApiSettings.CONFIG_KEY));
            services.Configure<AuthSettings>(Configuration.GetSection(AuthSettings.CONFIG_KEY));

            services.AddBusinessServices(Configuration);
            AddAutoMapper(services);

            services.AddCors(opts =>
            {
                opts.AddPolicy(
                    name: CORS_POLICY,
                    builder =>
                    {
                        builder
                            .WithOrigins(appConfig.ClientAppOrigins)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.DescribeAllParametersInCamelCase();
                c.SwaggerDoc(API_CODE, new OpenApiInfo { Title = swaggerConfig.SwaggerDoc.Title, Version = swaggerConfig.SwaggerDoc.Version });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "swagger";
                    c.DocumentTitle = "Blazor Exp API Documentation";
                    c.SwaggerEndpoint($"/swagger/{API_CODE}/swagger.json", "Blazor EXP API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CORS_POLICY);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            var websiteAssembly = Assembly.GetExecutingAssembly();
            var mappingProfiles = websiteAssembly.DefinedTypes
                .Where(ti => ti.Name.EndsWith("MappingProfile"))
                .Select(ti => ti.AsType()).ToArray();

            services.AddAutoMapper(mappingProfiles);
        }
    }
}