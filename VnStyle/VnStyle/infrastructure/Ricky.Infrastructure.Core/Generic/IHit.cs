namespace Ricky.Infrastructure.Core.Generic
{
    public interface IHit<TKey, TDoc>
    {
        TKey Id { get; set; }
        TDoc Document { get; set; }
    }
}
