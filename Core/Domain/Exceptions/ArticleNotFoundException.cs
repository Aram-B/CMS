namespace Domain.Exceptions
{
    public sealed class ArticleNotFoundException : NotFoundException
    {
        public ArticleNotFoundException(long articleId)
            : base($"The article with the identifier {articleId} was not found.")
        {
        }
    }
}
