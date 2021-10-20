using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Attributes;
using Services.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ArticlesController(IServiceManager serviceManager) => _serviceManager = serviceManager;
                
        /// <summary>
        /// Get filtered list of articles
        /// </summary>                
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArticleDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetArticlesAsync([FromQuery] GetArticlesFiltersDto getArticlesFilteringDto, CancellationToken cancellationToken)
        {
            var articles = await _serviceManager.ArticleService.GetArticlesAsync(getArticlesFilteringDto, cancellationToken);

            return Ok(articles);
        }

        /// <summary>
        /// Get article by Id
        /// </summary>
        [HttpGet("{articleId:long}")]
        [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetArticleByIdAsync(long articleId, CancellationToken cancellationToken)
        {
            var article = await _serviceManager.ArticleService.GetArticleByIdAsync(articleId, cancellationToken);

            return Ok(article);
        }

        /// <summary>
        /// Creates new article
        /// </summary>
        [ApiKey]
        [HttpPost]
        [ProducesResponseType(typeof(CreateArticleDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateArticleAsync([FromBody] CreateArticleDto article, CancellationToken cancellationToken)
        {
            await _serviceManager.ArticleService.CreateArticleAsync(article, cancellationToken);

            return CreatedAtAction(nameof(CreateArticleAsync), article);
        }

        /// <summary>
        /// Deletes article
        /// </summary>
        [ApiKey]
        [HttpDelete("{articleId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteArticleAsync(long articleId, CancellationToken cancellationToken)
        {
            await _serviceManager.ArticleService.DeleteArticleAsync(articleId, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Updates article if exist
        /// </summary>
        [ApiKey]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateArticleAsync([FromBody] UpdateArticleDto article, CancellationToken cancellationToken)
        {
            await _serviceManager.ArticleService.UpdateArticleAsync(article, cancellationToken);

            return Ok();
        }
    }
}
