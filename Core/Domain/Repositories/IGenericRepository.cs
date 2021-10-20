using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize, bool orderByDescending, string fieldName, CancellationToken cancellationToken = default);

        Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        void Add(T entity);

        void Remove(T entity);

        void Update(T entity);
    }
}
