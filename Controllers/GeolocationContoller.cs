using GeolocationAPI.Clients;
using GeolocationAPI.EF.Repositories;
using GeolocationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace GeolocationAPI.Controllers
{
    [ApiController]
    [Route(Endpoints.Geolocation)]
    public class GeolocationController : ControllerBase
    {
        private readonly IApiStackClient _client;
        private readonly GeolocationRepository _geolocationRepository;

        public GeolocationController(IApiStackClient client, GeolocationRepository geolocationRepository)
        {
            _client = client;
            _geolocationRepository = geolocationRepository;
        }

        [HttpGet(Endpoints.GetGeolocation)]
        public async Task<IActionResult> GetGeolocation(string ipAddress, CancellationToken cancellationToken)
        {
            var geolocation = await _client.GetGeolocationData(ipAddress, cancellationToken);
            return Ok(geolocation);
        }

        [HttpGet(Endpoints.GetGeolocationFromDb)]
        public async Task<IActionResult> GetGeolocationFromDb(string ipAddress, CancellationToken cancellationToken)
        {
            var geolocation = await _geolocationRepository.Get(ipAddress, cancellationToken);
            
            return Ok(geolocation);
        
        }
    }
}
