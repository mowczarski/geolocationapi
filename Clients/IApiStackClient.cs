using GeolocationAPI.Clients.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace GeolocationAPI.Clients
{
    public interface IApiStackClient
    {
        Task<ApiStackResponse> GetGeolocationData(string ipAddress, CancellationToken cancellationToken);
    }
}
