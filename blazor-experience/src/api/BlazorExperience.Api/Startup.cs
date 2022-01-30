using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
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
            services.Configure<ApiSettings>(Configuration.GetSection(AuthSettings.CONFIG_KEY));

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
                c.SwaggerDoc(API_CODE, new OpenApiInfo { Title = swaggerConfig.SwaggerDoc.Title, Version = swaggerConfig.SwaggerDoc.Version });
                c.AddSecurityDefinition(swaggerConfig.SecurityDefinition.Name, new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(swaggerConfig.SecurityDefinition.Authorisation.Url),
                            TokenUrl = new Uri(swaggerConfig.SecurityDefinition.Authorisation.TokenUrl),
                            Scopes = swaggerConfig.SecurityDefinition.Authorisation.Scopes
                        }
                    }
                });
                c.EnableAnnotations();
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
                    c.RoutePrefix = "api-docs";
                    c.DocumentTitle = "Blazor Exp API Documentation";
                    c.SwaggerEndpoint($"/swagger/{API_CODE}/swagger.json", "Blazor EXP API v1");

                    c.OAuthClientId("blazorexp-api-swagger");
                    c.OAuthAppName("Blazor Exp API Swagger UI");
                    c.OAuthUsePkce();
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
            
        }
    }
}