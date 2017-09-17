using Ricky.Infrastructure.Core.Generic;

namespace VnStyle.Services.Business.Models
{
    public class RootCate : BaseEntityName
    {
        public int MaxLevel { get; set; }
        public bool HasFeaturedImage { get; set; }
    }
}
