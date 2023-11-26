using Geolocation.Application.Abstractions.Messaging;
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
        public GetGeolocationQueryHandler()
        {
        }

        public async Task<Result<GetGeolocationResponse>> Handle(GetGeolocationQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(new GetGeolocationResponse());
        }
    }
}