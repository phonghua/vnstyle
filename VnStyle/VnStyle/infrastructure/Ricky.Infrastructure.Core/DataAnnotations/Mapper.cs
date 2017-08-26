using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Ricky.Infrastructure.Core.DataAnnotations
{
    public class Mapper
    {
        #region "Table name"
        
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName = new ConcurrentDictionary<RuntimeTypeHandle, string>();
        public static string TableName<T>()
        {
            return TableName(typeof(T));
        }
        public static string TableName(Type type)
        {
            var tableAttributeName = (typeof(TableAttribute)).FullName;
            string name;
            if (TypeTableName.TryGetValue(type.TypeHandle, out name)) return name;

            var tableattr = type.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().FullName == tableAttributeName) as dynamic;
            if (tableattr != null)
                name = tableattr.Name;
            else
            {
                name = type.Name;
                if (type.IsInterface && name.StartsWith("I"))
                    name = name.Substring(1);
            }
            

            TypeTableName[type.TypeHandle] = name;
            return name;
        }
        
        #endregion


    }
}
