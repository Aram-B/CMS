using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Ef.Configurations
{
    internal sealed class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable(nameof(Article));
            builder.HasKey(article => article.Id);
            builder.Property(article => article.Id).ValueGeneratedOnAdd();
        }
    }
}
