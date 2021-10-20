using Domain.Exceptions;
using DTOs;
using Persistence.Ef.Repositories;
using Services;
using Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ServiceManagerTests : IClassFixture<SeedDataFixture>
    {
        private readonly SeedDataFixture _seedDataFixture;
        private IServiceManager _serviceManager;

        public ServiceManagerTests(SeedDataFixture seedDataFixture)
        {
            _seedDataFixture = seedDataFixture;

            var repositoryManager = new RepositoryManager(_seedDataFixture.CmsDbContext);
            _serviceManager = new ServiceManager(repositoryManager);
        }

        [Fact]
        public async Task ServiceManager_Articles_Get_By_Id()
        {
            var article = await _serviceManager.ArticleService.GetArticleByIdAsync(1);

            Assert.NotNull(article);
            Assert.Equal(1, article.Id);
        }

        [Fact]
        public async Task ServiceManager_Articles_Get_Filtered()
        {
            var filters = new GetArticlesFiltersDto
            {
                OrderByDescending = true,
                OrderByField = "Id",
                PageNumber = 1,
                PageSize = 2
            };

            var filteredArticles = await _serviceManager.ArticleService.GetArticlesAsync(filters);

            Assert.NotEmpty(filteredArticles);
            Assert.Equal(2, filteredArticles.ToList()[0].Id);
            Assert.Equal(1, filteredArticles.ToList()[1].Id);
        }

        [Fact]
        public async Task ServiceManager_Articles_Get_By_Id_ArticleNotFoundException()
        {
            Func<Task> getArticleByIdAction = () => _serviceManager.ArticleService.GetArticleByIdAsync(7);

            await Assert.ThrowsAsync<ArticleNotFoundException>(getArticleByIdAction);
        }
    }
}
