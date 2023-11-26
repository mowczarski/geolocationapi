using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Domain.Abstrations
{
    public interface IGeolocalizationRepository
    {
        public Task AddAsync(Geolocalization geolocation, CancellationToken token);

        public void Remove(Geolocalization geolocation);

        public Task<Geolocalization> GetAsync(string address, CancellationToken cancellationToken);
    }
}
