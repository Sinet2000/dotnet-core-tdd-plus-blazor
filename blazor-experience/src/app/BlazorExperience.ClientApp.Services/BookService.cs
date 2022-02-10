using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorExperience.Shared.Constants;
using BlazorExperience.Shared.ViewModels;
using BlazorExperience.Shared.ViewModels.Book;

namespace BlazorExperience.ClientApp.Services
{
    public class BookService : BaseService<BookViewModel>, IBookService
    {
        public BookService(HttpClient httpClient) : base(httpClient, Routes.BookService)
        {

        }

        public async Task<List<BookDatatableViewModel>> GetDashboardData()
        {
            return await JsonSerializer.DeserializeAsync<List<BookDatatableViewModel>>
                (await HttpClient.GetStreamAsync(RequestUri), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }

    public interface IBookService : IBaseService<BookViewModel>
    {
        Task<List<BookDatatableViewModel>> GetDashboardData();
    }
}
