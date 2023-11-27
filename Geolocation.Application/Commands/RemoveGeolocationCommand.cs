using Geolocation.Application.Abstraction.Messaging;
using Geolocation.Domain;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGeolocalizationRepository _geolocalizationRepository;

        public RemoveGeolocationCommandHandler(IGeolocalizationRepository geolocalizationRepository, IUnitOfWork unitOfWork)
        {
            _geolocalizationRepository = geolocalizationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveGeolocationCommand command, CancellationToken cancellationToken)
        {
            var geolocation = await _geolocalizationRepository.GetAsync(command.Address, cancellationToken);

            if (geolocation != null)
            {
                _geolocalizationRepository.Remove(geolocation);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }

            return Result.Failure(GeolocationErrors.CannotRemove);
        } 
    }
}