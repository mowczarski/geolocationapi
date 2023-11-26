using Geolocation.Domain.Abstrations;
using MediatR;

namespace Geolocation.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}