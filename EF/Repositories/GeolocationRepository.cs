using GeolocationAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeolocationAPI.EF.Repositories
{
    public class GeolocationRepository
    {
        private DataContext _context { get; }

        public GeolocationRepository(DataContext dataContext) 
            => _context = dataContext;

        public void Add(Geolocation geolocation, CancellationToken token) 
            => _context.AddAsync(geolocation, token);
    
        public void Remove(Geolocation geolocation) 
            => _context.Remove(geolocation);

        public async Task<Geolocation> Get(string ipAddress, CancellationToken cancellationToken) 
            => await _context.Geolocations
                .Where(x => x.Ip.Equals(ipAddress))
                .Include(x => x.Locations)
                .ThenInclude(x => x.Languages)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
