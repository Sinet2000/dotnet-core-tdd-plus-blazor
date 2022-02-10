using System.Text;
using System.Text.Json;

namespace BlazorExperience.ClientApp.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected readonly HttpClient HttpClient;
        protected readonly string RequestUri;

        protected BaseService(HttpClient httpClient, string requestUri)
        {
            HttpClient = httpClient;
            RequestUri = requestUri;
        }

        public virtual async Task<T> Create(T model)
        {
            var modelJson =
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync(RequestUri, modelJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public virtual async Task<T> GetById(long id)
        {
            return await JsonSerializer.DeserializeAsync<T>
                (await HttpClient.GetStreamAsync($"{RequestUri}/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public virtual async Task Update(T model)
        {
            var modelJson =
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            await HttpClient.PutAsync(RequestUri, modelJson);
        }

        public virtual async Task Delete(long id)
        {
            await HttpClient.DeleteAsync($"{RequestUri}/{id}");
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<T>>
                (await HttpClient.GetStreamAsync(RequestUri), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }

    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(long id);
        Task<T> Create(T model);
        Task Update(T model);
        Task Delete(long id);
    }
}
