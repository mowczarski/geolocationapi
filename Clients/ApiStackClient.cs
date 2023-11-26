using GeolocationAPI.Authentication;
using GeolocationAPI.Clients.Contracts;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GeolocationAPI.Clients
{
    public class ApiStackClient : IApiStackClient
    {
        private readonly HttpClient _httpClient;
        private readonly ApiStackConfiguration _apiStackConfiguration;

        public ApiStackClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiStackConfiguration = configuration.GetSection(nameof(ApiStackConfiguration)).Get<ApiStackConfiguration>();
        }

        public async Task<ApiStackResponse> GetGeolocationData(string ipAddress, CancellationToken cancellationToken)
            => await _httpClient.GetFromJsonAsync<ApiStackResponse>($"{ipAddress}?access_key={_apiStackConfiguration.ApiKey}", cancellationToken);
    }
}

