using System.Threading;
using System.Threading.Tasks;

namespace GeolocationAPI.EF
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
