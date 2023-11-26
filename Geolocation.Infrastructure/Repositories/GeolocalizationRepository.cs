using Geolocation.Domain;
using Geolocation.Domain.Abstrations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Repositories
{
    public class GeolocalizationRepository : IGeolocalizationRepository
    {
        private DataContext _context { get; }

        public GeolocalizationRepository(DataContext dataContext)
            => _context = dataContext;

        public async Task AddAsync(Geolocalization geolocation, CancellationToken token)
            => await _context.AddAsync(geolocation, token);

        public void Remove(Geolocalization geolocation)
            => _context.Remove(geolocation);

        public async Task<Geolocalization> GetAsync(string address, CancellationToken cancellationToken)
            => await _context.Geolocalizations
                .Where(x => x.Url != null && x.Url.Equals(address)  || x.Ip.Equals(address))
                .Include(x => x.Location)
                .ThenInclude(x => x.Languages)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
