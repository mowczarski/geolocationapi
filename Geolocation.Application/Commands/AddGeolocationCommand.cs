using Geolocation.Application.Messaging;
using Geolocation.Domain;
using Geolocation.Domain.Abstrations;
using System.Linq;
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
        private readonly IApiStackClient _apiStackClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGeolocalizationRepository _geolocalizationRepository;

        public AddGeolocationQueryHandler(IApiStackClient apiStackClient, IGeolocalizationRepository geolocalizationRepository, IUnitOfWork unitOfWork)
        {
            _apiStackClient = apiStackClient;
            _geolocalizationRepository = geolocalizationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddGeolocationCommand command, CancellationToken cancellationToken)
        {
            var geolocationData = await _apiStackClient.GetGeolocationDataAsync(command.Address, cancellationToken);

            if (geolocationData != null)
            {
                await _geolocalizationRepository.AddAsync(
                    new Geolocalization(
                        geolocationData.type,
                        geolocationData.ip,
                        command.Address.Equals(geolocationData.ip) ? null : command.Address,
                        geolocationData.latitude,
                        geolocationData.longitude,
                        geolocationData.continent_name,
                        geolocationData.country_name,
                        geolocationData.region_name,
                        geolocationData.city,
                        geolocationData.zip,
                        new Location(
                            geolocationData.location.geoname_id,
                            geolocationData.location.country_flag,
                            geolocationData.location.calling_code,
                            geolocationData.location.is_eu,
                            geolocationData.location.languages.Select(y => new Language(y.code, y.name, y.name)).ToList()))
                        , cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }

            return Result.Failure(Error.NullValue);
        }
    }
}