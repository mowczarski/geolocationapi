using Geolocation.Domain.Abstrations;
using MediatR;

namespace Geolocation.Application.Abstraction.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
    {
    }
}