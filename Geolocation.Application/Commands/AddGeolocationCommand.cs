using Geolocation.Application.Abstraction.Messaging;
using Geolocation.Domain;
using Geolocation.Domain.Abstrations;
using MediatR;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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

    public class AddGeolocationCommandHandler : ICommandHandler<AddGeolocationCommand>
    {
        private readonly IApiStackClient _apiStackClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGeolocalizationRepository _geolocalizationRepository;

        public AddGeolocationCommandHandler(IApiStackClient apiStackClient, IGeolocalizationRepository geolocalizationRepository, IUnitOfWork unitOfWork)
        {
            _apiStackClient = apiStackClient;
            _geolocalizationRepository = geolocalizationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddGeolocationCommand command, CancellationToken cancellationToken)
        {
            var result = await _geolocalizationRepository.GetAsync(command.Address, cancellationToken);

            if (result != null)
                return Result.Failure(GeolocationErrors.DuplicateError);

            string address = command.Address;

            if (Regex.IsMatch(address, @"^https?:\/\/", RegexOptions.IgnoreCase))
                address = address.Substring(8);

            var geolocationData = await _apiStackClient.GetGeolocationDataAsync(address, cancellationToken);

            if (geolocationData != null && geolocationData.ip != null)
            {
                await _geolocalizationRepository.AddAsync(
                    new Geolocalization(
                        command.Address.Equals(geolocationData.ip) ? null : command.Address,
                        geolocationData.type,
                        geolocationData.ip,
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
                            geolocationData.location.languages
                                .Select(y => new Language(y.code, y.name, y.name)).ToList())
                        ), cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }

            return Result.Failure(GeolocationErrors.ApiStackError);
        }
    }
}