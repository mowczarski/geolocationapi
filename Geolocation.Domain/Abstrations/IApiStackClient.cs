using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Domain.Abstrations
{
    public interface IApiStackClient
    {
        Task<ApiStackResponse> GetGeolocationDataAsync(string ipAddress, CancellationToken cancellationToken);
        Task<ApiStackResponse> CheckAsync(CancellationToken cancellationToken);
    }
}
