using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Ricky.Infrastructure.Core.ObjectContainer.Dependency;

namespace Ricky.Infrastructure.Core.ObjectContainer.Autofac.DependencyManagement
{
    public static class ContainerManagerExtensions
    {
        public static global::Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> PerLifeStyle<TLimit, TActivatorData, TRegistrationStyle>(
            this global::Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder, Ricky.Infrastructure.Core.ObjectContainer.Dependency.ComponentLifeStyle lifeStyle)
        {
            switch (lifeStyle)
            {
                case ComponentLifeStyle.InRequestScope:
                    //return HttpContext.Current != null ? builder.InstancePerHttpRequest() : builder.InstancePerLifetimeScope();
                    return HttpContext.Current != null ? builder.InstancePerRequest() : builder.InstancePerLifetimeScope();

                //return builder.InstancePerLifetimeScope();
                case ComponentLifeStyle.Transient:
                    return builder.InstancePerDependency();
                case ComponentLifeStyle.Singleton:
                    return builder.SingleInstance();
                default:
                    return builder.SingleInstance();
            }
        }


    }
}