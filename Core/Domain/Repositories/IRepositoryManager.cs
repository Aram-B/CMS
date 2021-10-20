namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IArticleRepository ArticleRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
