using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Ef;
using System;

namespace UnitTests
{
    public class SeedDataFixture : IDisposable
    {
        public CmsDbContext CmsDbContext { get; private set; }

        public SeedDataFixture()
        {
            var options = new DbContextOptionsBuilder<CmsDbContext>()
                .UseInMemoryDatabase("Articles")
                .Options;

            CmsDbContext = new CmsDbContext(options);


            var article1 = new Article { Title = "Article1", Body = "Test Article 1" };
            var article2 = new Article { Title = "Article2", Body = "Test Article 2" };
            var article3 = new Article { Title = "Article3", Body = "Test Article 3" };
            var article4 = new Article { Title = "Article4", Body = "Test Article 4" };
            var article5 = new Article { Title = "Article5", Body = "Test Article 5" };

            CmsDbContext.Articles.AddRange(article1, article2, article3, article4, article5);
            CmsDbContext.SaveChanges();
        }

        public void Dispose()
        {
            CmsDbContext.Dispose();   
        }
    }
}
