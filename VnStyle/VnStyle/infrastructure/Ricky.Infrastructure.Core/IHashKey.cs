namespace Ricky.Infrastructure.Core
{
    public interface IHashKey
    {
        string HashEncodeId(int pId);
        int? HashDecodeId(string hashValue);

        string HashEncodeLongId(long pId);
        long? HashDecodeLongId(string hashValue);
    }
}
