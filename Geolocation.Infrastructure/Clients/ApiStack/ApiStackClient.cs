using Geolocation.Domain;
using Geolocation.Domain.Abstrations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Clients.ApiStack
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

        public async Task<ApiStackResponse> GetGeolocationDataAsync(string address, CancellationToken cancellationToken)
            => await _httpClient.GetFromJsonAsync<ApiStackResponse>($"{address}?access_key={_apiStackConfiguration.ApiKey}", cancellationToken);

        public async Task<ApiStackResponse> CheckAsync(CancellationToken cancellationToken)
            => await _httpClient.GetFromJsonAsync<ApiStackResponse>($"check?access_key={_apiStackConfiguration.ApiKey}", cancellationToken);
    }
}

