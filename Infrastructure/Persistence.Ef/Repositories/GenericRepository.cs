using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Ef.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Ef.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CmsDbContext _cmsDbContext;

        public GenericRepository(CmsDbContext cmsDbContext) => _cmsDbContext = cmsDbContext;

        public async Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize, bool orderByDescending, 
                                                      string fieldName, CancellationToken cancellationToken = default)
        {
            return await _cmsDbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .AsQueryable()
                .OrderBy(fieldName, orderByDescending)
                .ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        { 
            var resultEntity = await _cmsDbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
            if (resultEntity != null)
                _cmsDbContext.Entry(resultEntity).State = EntityState.Detached;

            return resultEntity;
        }            

        public void Add(T entity)
        {
            _cmsDbContext.Set<T>().Add(entity);
        }
                
        public void Remove(T entity)
        {
            _cmsDbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _cmsDbContext.Set<T>().Update(entity);
        }


    }
}
