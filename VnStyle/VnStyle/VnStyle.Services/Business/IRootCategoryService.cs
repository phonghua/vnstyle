using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ricky.Infrastructure.Core.Generic;

namespace VnStyle.Services.Business
{
    public interface IRootCategoryService
    {
        List<BaseEntityName> GetAllRootCategories();
    }
}


