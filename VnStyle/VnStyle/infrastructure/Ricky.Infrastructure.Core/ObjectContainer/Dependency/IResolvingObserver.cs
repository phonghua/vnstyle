namespace Ricky.Infrastructure.Core.ObjectContainer.Dependency
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResolvingObserver
    {
        int Order { get; }
        object OnResolved(object resolvedObject);
    }
}
