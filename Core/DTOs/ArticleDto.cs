using System;

namespace DTOs
{
    public sealed class ArticleDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime? CreatedAt { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;
    }
}
