using Geolocation.Domain.Abstrations;
using MediatR;

namespace Geolocation.Application.Messaging 
{ 
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    {
    }

    public interface IBaseCommand
    {
    }
}