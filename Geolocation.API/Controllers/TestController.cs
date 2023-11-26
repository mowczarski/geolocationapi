using Geolocation.API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Geolocation.API.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route(Endpoints.Test)]
        public IActionResult Test()
        {
            return Ok("API Works!!");
        }
    }
}
