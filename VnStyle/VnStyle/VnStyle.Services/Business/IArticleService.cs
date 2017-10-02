using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.Generic;
using System.Collections.Generic;
using VnStyle.Services.Business.Models;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Services.Business
{
    public interface IArticleService
    {
        IPagedList<ArticleListingModel> GetArticles(GetArticlesRequest request);
        ArticleDetailModel GetArticleIntro();
        ArticleDetailModel GetArticleById(int id);
        IPagedList<ArticleListingModel> GetNewArticles(GetArticlesRequest request);
        IList<ArticleListingModel> GetSession(bool flag); // request == true => get session1 
        ArticleListingModel GetFirstHomePageFeaturedArticles();
        IEnumerable<ArticleListingModel> GetLastHomePageFeaturedArticles();
        
        IPagedList<ArticleListingModel> GetArticlesByString(string search, PagingRequest request);


    }
}
