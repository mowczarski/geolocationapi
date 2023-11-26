using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GeolocationAPI.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("TestConnection")]
        public IActionResult Test()
        {
            Log.Information("API Works!!");
            return Ok("API Works!!");
        }
    }
}
