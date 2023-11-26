using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Application.Abstraction.Logging
{
    public class Logging<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            try
            {
                Log.Information($"Executing {name}");

                var result = await next();

                Log.Information($"{name} processed successfully");

                return result;

            }
            catch (Exception exception)
            {
                Log.Error(exception, $"{name} processing failed");
                throw;
            }
        }
    }
}
