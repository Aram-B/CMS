using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using DTOs;
using Mapster;
using Services.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class ArticleService : IArticleService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ArticleService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task<IEnumerable<ArticleDto>> GetArticlesAsync(GetArticlesFiltersDto articleFilters, CancellationToken cancellationToken = default)
        {
            var articles = await _repositoryManager.ArticleRepository.GetAllAsync(articleFilters.PageNumber, articleFilters.PageSize, 
                                                                                  articleFilters.OrderByDescending, articleFilters.OrderByField, cancellationToken);                        

            return articles.Adapt<IEnumerable<ArticleDto>>();
        }

        public async Task<ArticleDto> GetArticleByIdAsync(long articleId, CancellationToken cancellationToken = default)
        {
            var article = await _repositoryManager.ArticleRepository.GetByIdAsync(articleId, cancellationToken);

            if (article == null)
                throw new ArticleNotFoundException(articleId);

            return article.Adapt<ArticleDto>();
        }

        public async Task CreateArticleAsync(CreateArticleDto createArticleDto, CancellationToken cancellationToken = default)
        {
            var articleDto = new ArticleDto
            {
                Title = createArticleDto.Title,
                Body = createArticleDto.Body,
                CreatedAt = System.DateTime.UtcNow
            };

            var article = articleDto.Adapt<Article>();

            _repositoryManager.ArticleRepository.Add(article);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(long articleId, CancellationToken cancellationToken = default)
        {
            var article = await _repositoryManager.ArticleRepository.GetArticleByIdAsync(articleId);

            if (article == null)
                throw new ArticleNotFoundException(articleId);
                        
            _repositoryManager.ArticleRepository.Remove(article);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }
                
        public async Task UpdateArticleAsync(UpdateArticleDto updateArticleDto, CancellationToken cancellationToken = default)
        {
            var existingArticle = await _repositoryManager.ArticleRepository.GetArticleByIdAsync(updateArticleDto.Id, cancellationToken);

            if (existingArticle == null)
                throw new ArticleNotFoundException(updateArticleDto.Id);

            var articleDto = new ArticleDto
            {
                Id = updateArticleDto.Id,
                Title = updateArticleDto.Title,
                Body = updateArticleDto.Body,
                CreatedAt = existingArticle.CreatedAt,
                UpdatedAt = System.DateTime.UtcNow
            };

            var article = articleDto.Adapt<Article>();

            _repositoryManager.ArticleRepository.Update(article);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }
    }
}
