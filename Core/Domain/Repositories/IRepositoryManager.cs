using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IGenericRepository<Article> ArticleRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
