using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IArticleRepository : IGenericRepository<Article>
    {
        Task<Article> GetArticleByIdAsync(long articleId, CancellationToken cancellationToken = default);
    }
}
