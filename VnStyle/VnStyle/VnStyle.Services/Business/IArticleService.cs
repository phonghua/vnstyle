using Ricky.Infrastructure.Core;
using System.Collections.Generic;
using VnStyle.Services.Business.Models;

namespace VnStyle.Services.Business
{
    public interface IArticleService
    {
        IPagedList<ArticleModelView> GetArticles(ArticleModelRequest request);
        ArticleModelView GetArticleIntro(ArticleModelRequest request);
        ArticleModelView GetArticleById(int? id,ArticleModelRequest request);
        IPagedList<ArticleModelView> GetArticlesNew(ArticleModelRequest request);
    }
}
