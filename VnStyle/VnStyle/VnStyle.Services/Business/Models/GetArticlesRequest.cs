using Ricky.Infrastructure.Core.Generic;

namespace VnStyle.Services.Business.Models
{
    public class GetArticlesRequest : PagingRequest
    {
        public int RootCate { get; set; }
    }
    public enum eSession
    {
        Session1 = 1,
        Session2 = 2
    }
}
