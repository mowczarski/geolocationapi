using Geolocation.Domain.Abstrations;
using MediatR;

namespace Geolocation.Application.Abstraction.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}