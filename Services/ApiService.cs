using Newtonsoft.Json;

namespace GestionVoitureFrontOffice.Services
{
    public class ApiService(HttpClient httpClient)
    {
        public async Task<T?> GetDataFromApiAsync<T>(string apiUrl)
        {
            var response = await httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode) return default(T);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);

        }
    }
}
