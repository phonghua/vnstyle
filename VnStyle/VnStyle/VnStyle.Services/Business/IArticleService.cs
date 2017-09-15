using Ricky.Infrastructure.Core;
using System.Collections.Generic;
using VnStyle.Services.Business.Models;

namespace VnStyle.Services.Business
{
    public interface IArticleService
    {
        IPagedList<ArticleListingModel> GetArticles(GetArticlesRequest request);
        ArticleDetailModel GetArticleIntro();
        ArticleDetailModel GetArticleById(int id);
    }
}
