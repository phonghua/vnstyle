using Autofac;

namespace Ricky.Infrastructure.Core.ObjectContainer.Autofac.DependencyManagement
{
    public interface IAutofacDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}
