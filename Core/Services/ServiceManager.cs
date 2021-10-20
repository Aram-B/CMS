using Domain.Repositories;
using Services.Abstractions;
using System;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IArticleService> _articleService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _articleService = new Lazy<IArticleService>(() => new ArticleService(repositoryManager));
        }

        public IArticleService ArticleService => _articleService.Value;
    }
}
