using Geolocation.API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Geolocation.API.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route(Endpoints.Test)]
        public IActionResult Test()
        {
            Log.Information("API Works!!");
            return Ok("API Works!!");
        }

        [HttpGet]
        [Route(Endpoints.Healthcheck)]
        public IActionResult Healthcheck()
        {
            Log.Information("Healthchecks !!");
            return Ok("Run Healthcheck here!!");
        }
    }
}
