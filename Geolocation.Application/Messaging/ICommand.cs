using Geolocation.Domain.Abstrations;
using MediatR;

namespace Geolocation.Application.Messaging 
{ 
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}