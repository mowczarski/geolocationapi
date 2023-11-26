using Geolocation.Application.Abstractions.Messaging;
using Geolocation.Domain.Abstrations;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Application 
{
    public class AddGeolocationCommand : ICommand
    {
        public string Address { get; set; }

        public AddGeolocationCommand(string address)
        {
            Address = address;
        }
    }

    public class AddGeolocationQueryHandler : ICommandHandler<AddGeolocationCommand>
    {
        public async Task<Result> Handle(AddGeolocationCommand command, CancellationToken cancellationToken)
        {
            return Result.Success();
        }
    }
}