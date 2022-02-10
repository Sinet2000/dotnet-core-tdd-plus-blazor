using System.Text.Json;
using BlazorExperience.Shared.Constants;
using BlazorExperience.Shared.ViewModels.Film;

namespace BlazorExperience.ClientApp.Services
{
    public class FilmService : BaseService<FilmViewModel>, IFilmService
    {
        public FilmService(HttpClient httpClient) : base(httpClient, Routes.FilmService)
        {

        }

        public async Task<List<FilmDatatableViewModel>> GetDashboardData()
        {
            return await JsonSerializer.DeserializeAsync<List<FilmDatatableViewModel>>
                (await HttpClient.GetStreamAsync(RequestUri), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }

    public interface IFilmService : IBaseService<FilmViewModel>
    {
        Task<List<FilmDatatableViewModel>> GetDashboardData();
    }
}
