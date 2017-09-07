using System.Collections.Generic;
using Ricky.Infrastructure.Core.Generic;
using VnStyle.Services.Data.Enum;

namespace VnStyle.Services.Business
{
    public class RootCategoryService : IRootCategoryService
    {
        private readonly IResourceService _resourceService;


        public RootCategoryService(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public List<BaseEntityName> GetAllRootCategories()
        {
            var resKey = "RootCate_";
            var cates = new List<BaseEntityName>
            {
                //new BaseEntityName {Id = (int)ERootCategory.Article, Name = _resourceService.T(resKey + ERootCategory.Article.ToString() )},
                new BaseEntityName {Id = (int)ERootCategory.Intro, Name = _resourceService.T(resKey + ERootCategory.Intro )},
                new BaseEntityName {Id = (int)ERootCategory.Event, Name = _resourceService.T(resKey + ERootCategory.Event )},
                new BaseEntityName {Id = (int)ERootCategory.Image, Name = _resourceService.T(resKey + ERootCategory.Image )},
                new BaseEntityName {Id = (int)ERootCategory.Course, Name = _resourceService.T(resKey + ERootCategory.Course )},
                new BaseEntityName {Id = (int)ERootCategory.Tattoo, Name = _resourceService.T(resKey + ERootCategory.Tattoo )},
                new BaseEntityName {Id = (int)ERootCategory.Piercing, Name = _resourceService.T(resKey + ERootCategory.Piercing )},
            };
            return cates;
        }
    }
}