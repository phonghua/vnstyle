namespace Ricky.Infrastructure.Core.Generic
{
    public class PagingRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }

        public void EnsurePagingRequestValid(int defaultPageSize = 20)
        {
            if (PageIndex < 0) PageIndex = 0;
            if (PageSize < 1) PageSize = defaultPageSize;
        }
    }
}
