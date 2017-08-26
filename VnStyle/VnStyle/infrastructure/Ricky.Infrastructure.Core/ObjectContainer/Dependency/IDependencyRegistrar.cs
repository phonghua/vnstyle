namespace Ricky.Infrastructure.Core.ObjectContainer.Dependency
{
    public interface IDependencyRegistrar
    {
        void Register(IContainerManager containerManager, ITypeFinder typeFinder);

        int Order { get; }
    }
}
