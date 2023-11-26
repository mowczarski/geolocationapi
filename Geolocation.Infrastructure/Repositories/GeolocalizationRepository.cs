using Geolocation.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure
{
    public class GeolocalizationRepository : IGeolocalizationRepository
    {
        private DataContext _context { get; }

        public GeolocalizationRepository(DataContext dataContext) 
            => _context = dataContext;

        public async Task Add(Geolocalization geolocation, CancellationToken token) 
            => await _context.AddAsync(geolocation, token);
    
        public void Remove(Geolocalization geolocation) 
            => _context.Remove(geolocation);

        public async Task<Geolocalization> Get(string ipAddress, CancellationToken cancellationToken) 
            => await _context.Geolocalizations
                .Where(x => x.Ip.Equals(ipAddress))
                .Include(x => x.Locations)
                .ThenInclude(x => x.Languages)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
