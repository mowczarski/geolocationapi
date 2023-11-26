using Microsoft.AspNetCore.Mvc;

namespace GeolocationAPI.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("TestConnection")]
        public IActionResult Test()
        {
           return Ok("API Works!!");
        }
    }
}
