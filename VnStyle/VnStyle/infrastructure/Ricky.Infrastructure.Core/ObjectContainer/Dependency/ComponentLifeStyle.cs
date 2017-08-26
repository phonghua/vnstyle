namespace Ricky.Infrastructure.Core.ObjectContainer.Dependency
{
    public enum ComponentLifeStyle
    {
        Singleton = 0,
        Transient = 1,
        InThreadScope = 2,
        InRequestScope = 3
    }
}
