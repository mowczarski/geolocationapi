using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Domain
{
    public interface IGeolocalizationRepository
    {
        public Task Add(Geolocalization geolocation, CancellationToken token);

        public void Remove(Geolocalization geolocation);

        public Task<Geolocalization> Get(string ipAddress, CancellationToken cancellationToken);
    }
}
