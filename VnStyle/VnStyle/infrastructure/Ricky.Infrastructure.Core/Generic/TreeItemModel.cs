using System.Collections.Generic;
using System.Security.AccessControl;

namespace Ricky.Infrastructure.Core.Generic
{
    public class TreeItemModel<T>
    {
        public T Node { get; set; }
        public List<TreeItemModel<T>> SubItems { get; set; }
    }

    public class TreeNode<T>
    {
        public T Id { get; set; }
        public string NodeName { get; set; }
        public T ParentId { get; set; }
        public int Seq { get; set; }
    }
}
