using Domain.Repositories;
using System;

namespace Persistence.Ef.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IArticleRepository> _articleRepository;
        private readonly Lazy<IUnitOfWork> _unitOfWork;

        public RepositoryManager(CmsDbContext cmsDbContext)
        {
            _articleRepository = new Lazy<IArticleRepository>(() => new ArticleRepository(cmsDbContext));
            _unitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(cmsDbContext));
        }

        public IArticleRepository ArticleRepository => _articleRepository.Value;

        public IUnitOfWork UnitOfWork => _unitOfWork.Value;
    }
}
