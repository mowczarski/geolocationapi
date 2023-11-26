using Geolocation.Domain.Abstrations;
using MediatR;

namespace Geolocation.Application.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}