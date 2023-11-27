using Geolocation.Application.Abstraction.Messaging;
using Geolocation.Domain.Abstrations;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Application
{
    public class GetGeolocationQuery : IQuery<GetGeolocationResponse>
    {
        public string Address { get; }

        public GetGeolocationQuery(string address)
        {
            Address = address;
        }
    }

    public class GetGeolocationQueryHandler : IQueryHandler<GetGeolocationQuery, GetGeolocationResponse>
    {
        private readonly IGeolocalizationRepository _geolocalizationRepository;

        public GetGeolocationQueryHandler(IGeolocalizationRepository geolocalizationRepository)
        {
            _geolocalizationRepository = geolocalizationRepository;
        }

        public async Task<Result<GetGeolocationResponse>> Handle(GetGeolocationQuery request, CancellationToken cancellationToken)
        {
            var result = await _geolocalizationRepository.GetAsync(request.Address, cancellationToken);

            if (result != null)
            {
                return Result.Success(new GetGeolocationResponse(
                    result.Latitude,
                    result.Longitude,
                    result.Continent,
                    result.Country,
                    result.Region,
                    result.City,
                    result.CityCode));
            }  
            
          return Result.Failure<GetGeolocationResponse>(GeolocationErrors.NotFound);         
        }
    }
}