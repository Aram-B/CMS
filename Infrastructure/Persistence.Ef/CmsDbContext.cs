using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Ef
{
    public sealed class CmsDbContext : DbContext
    {
        public CmsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CmsDbContext).Assembly);
    }
}
