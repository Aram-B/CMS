namespace DTOs
{
    public sealed class GetArticlesFiltersDto
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string OrderByField { get; set; }

        public bool OrderByDescending { get; set; } = false;      
    }
}
