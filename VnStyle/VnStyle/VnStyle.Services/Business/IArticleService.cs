using System.Collections.Generic;
using VnStyle.Services.Business.Models;

namespace VnStyle.Services.Business
{
    public interface IArticleService
    {
        IEnumerable<ArticleModelView> GetArticles(ArticleModelRequest request);
        ArticleModelView GetArticleIntro(ArticleModelRequest request);
        ArticleModelView GetArticleById(int id,ArticleModelRequest request);

    }
}
