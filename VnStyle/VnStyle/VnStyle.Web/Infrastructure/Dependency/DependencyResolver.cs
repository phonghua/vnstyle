using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ricky.Infrastructure.Core.ObjectContainer;

namespace VnStyle.Web.Infrastructure.Dependency
{
    public class DependencyResolver : IDependencyResolver, IServiceLocator
    {
        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null) return null;
            try
            {
                return EngineContext.Current.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return EngineContext.Current.ResolveAll(serviceType);
        }
    }
}