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
    }
}
