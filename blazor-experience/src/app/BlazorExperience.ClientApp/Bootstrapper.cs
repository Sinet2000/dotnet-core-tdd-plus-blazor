using BlazorExperience.ClientApp.Services;
using BlazorExperience.ClientApp.Settings;

namespace BlazorExperience.ClientApp
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddClientAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = new AppSettings();
            configuration.Bind(AppSettings.CONFIG_KEY, appSettings);
            var apiUri = new Uri(appSettings.ApiAddress);

            services.Configure<AppSettings>(configuration.GetSection(AppSettings.CONFIG_KEY));
            

            services.AddAppHttpClients(apiUri);

            services.AddScoped(sp => new HttpClient { BaseAddress = apiUri });

            return services;
        }

        private static void RegisterTypedClient<TClient, TImplementation>(this IServiceCollection services, Uri apiBaseUrl)
            where TClient : class
            where TImplementation : class, TClient
        {
            services
                .AddHttpClient<TClient, TImplementation>(client =>
                {
                    client.BaseAddress = apiBaseUrl;
                });
        }

        private static void AddAppHttpClients(this IServiceCollection services, Uri apiBaseUrl)
        {
            services.RegisterTypedClient<IBookService, BookService>(apiBaseUrl);
            services.RegisterTypedClient<IFilmService, FilmService>(apiBaseUrl);
        }
    }
}
