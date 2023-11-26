using Geolocation.API.Controllers.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Geolocation.Application;

namespace Geolocation.API.Controllers
{
    [ApiController]
    [Route(Endpoints.Geolocation)]
    public class GeolocationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GeolocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Produces(typeof(GetGeolocationResponse))]
        public async Task<IActionResult> GetGeolocation([FromQuery] GeolocationRequest request, CancellationToken cancellationToken)
        {
            var query = new GetGeolocationQuery(request.Address.Trim());
            var result = await _mediator.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddGeolocation([FromBody] GeolocationRequest request, CancellationToken cancellationToken)
        {
            var command = new AddGeolocationCommand(request.Address.Trim());
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsFailure ? BadRequest(result.Error): Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGeolocation([FromBody] GeolocationRequest request, CancellationToken cancellationToken)
        {
            var command = new RemoveGeolocationCommand(request.Address.Trim());
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsFailure ? BadRequest(result.Error) : Ok();
        }
    }
}
