using Ricky.Infrastructure.Core.ObjectContainer;
using Ricky.Infrastructure.Core.ObjectContainer.Autofac;

namespace VnStyle.Web
{
    public static class IoC
    {
        public static void RegisterResolver()
        {
            var engine = (AutofacEngine)EngineContext.Initialize(true, typeof(AutofacEngine), new WebAppTypeFinder());
        }
    }
}