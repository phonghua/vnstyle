using System;

namespace Ricky.Infrastructure.Core.Generic
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }

    public class BaseEntity : BaseEntity<int>
    {
    }

    public class BaseEntityName<T> : BaseEntity<T>
    {
        public String Name { get; set; }
    }

    public class BaseEntityName<TId,TValue> : BaseEntity<TId>
    {
        public TValue Name { get; set; }
    }

    public class BaseEntityName : BaseEntityName<int>
    {
        
    }

}
