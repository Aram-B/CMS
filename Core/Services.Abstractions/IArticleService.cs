using DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetArticlesAsync(GetArticlesFiltersDto getArticlesFilteringDto, CancellationToken cancellationToken = default);

        Task<ArticleDto> GetArticleByIdAsync(long articleId, CancellationToken cancellationToken = default);

        Task CreateArticleAsync(CreateArticleDto createArticleDto, CancellationToken cancellationToken = default);

        Task DeleteArticleAsync(long articleId, CancellationToken cancellationToken = default);

        Task UpdateArticleAsync(UpdateArticleDto updateArticleDto, CancellationToken cancellationToken = default);
    }
}
