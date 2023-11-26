using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Domain
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
