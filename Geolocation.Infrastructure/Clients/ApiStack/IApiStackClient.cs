using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.API.Clients.ApiStack
{
    public interface IApiStackClient
    {
        Task<ApiStackResponse> GetGeolocationData(string ipAddress, CancellationToken cancellationToken);
    }
}
