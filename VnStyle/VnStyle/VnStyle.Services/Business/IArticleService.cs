using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnStyle.Services.Business.Models;
using VnStyle.Services.Data.Enum;

namespace VnStyle.Services.Business
{
    public interface IArticleService
    {
        IEnumerable<ArticleModelView> GetArticles(ArticleModelRequest request);
        ArticleModelView GetArticleIntro(ArticleModelRequest request);
        ArticleModelView GetArticleById(int? id,ArticleModelRequest request);

    }
}
