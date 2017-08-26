using System;
using System.Collections.Generic;

namespace Ricky.Infrastructure.Core.ObjectContainer.Dependency
{
    public static class ContainerManagerExtensions
    {
        public static void AddComponent(this IContainerManager containerManager, Type service, IEnumerable<Type> implementations)
        {
            foreach (var item in implementations)
            {
                containerManager.AddComponent(service, item, item.FullName);
            }
        }
    }
}
