using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Ef.Repositories
{
    internal sealed class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {        
        public ArticleRepository(CmsDbContext cmsDbContext) 
            : base(cmsDbContext)
        {            
        }

        public async Task<Article> GetArticleByIdAsync(long articleId, CancellationToken cancellationToken = default)
        {
            var article = await base.GetByIdAsync(articleId, cancellationToken);
            _cmsDbContext.Entry(article).State = EntityState.Detached;

            return article;
        }
    }
}
