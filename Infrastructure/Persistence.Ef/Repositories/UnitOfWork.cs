using Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Ef.Repositories
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly CmsDbContext _cmsDbContext;

        public UnitOfWork(CmsDbContext cmsDbContext) => _cmsDbContext = cmsDbContext;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _cmsDbContext.SaveChangesAsync(cancellationToken);
    }
}
