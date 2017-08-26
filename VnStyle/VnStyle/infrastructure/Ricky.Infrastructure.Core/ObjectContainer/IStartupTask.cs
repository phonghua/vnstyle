namespace Ricky.Infrastructure.Core.ObjectContainer
{
    public interface IStartupTask 
    {
        void Execute();

        int Order { get; }
    }
}
