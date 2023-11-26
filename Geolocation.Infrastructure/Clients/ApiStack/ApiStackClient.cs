using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.API.Clients.ApiStack
{
    public class ApiStackClient : IApiStackClient
    {
        private readonly HttpClient _httpClient;
        private readonly IApiStackConfiguration _apiStackConfiguration;

        public ApiStackClient(HttpClient httpClient, IApiStackConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiStackConfiguration = configuration;
        }

        public async Task<ApiStackResponse> GetGeolocationData(string ipAddress, CancellationToken cancellationToken)
            => await _httpClient.GetFromJsonAsync<ApiStackResponse>($"{ipAddress}?access_key={_apiStackConfiguration.ApiKey}", cancellationToken);
    }
}

