using Geolocation.Application.Abstractions.Messaging;
using Geolocation.Domain.Abstrations;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Application 
{ 
    public class RemoveGeolocationCommand : ICommand
    {
        public string Address { get; set; }

        public RemoveGeolocationCommand(string address)
        {
            Address = address;
        }
    }

    public class RemoveGeolocationCommandHandler : ICommandHandler<RemoveGeolocationCommand>
    {
        public async Task<Result> Handle(RemoveGeolocationCommand command, CancellationToken cancellationToken)
        {
            return Result.Success();
        }
    }
}