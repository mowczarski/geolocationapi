using GeolocationAPI.Clients;
using GeolocationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace GeolocationAPI.Controllers
{
    [ApiController]
    [Route(Endpoints.Geolocation)]
    public class GeolocationController : ControllerBase
    {
        private readonly IApiStackClient _client;

        public GeolocationController(IApiStackClient client)
        {
            _client = client;
        }

        [HttpGet(Endpoints.GetGeolocation)]
        public async Task<IActionResult> GetGeolocation(string ipAddress, CancellationToken token)
        {
            var geolocation = await _client.GetGeolocationData(ipAddress, token);
            return Ok(geolocation);
        }
    }
}
