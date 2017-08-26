using System;
using System.Linq;
using Ricky.Infrastructure.Core;

namespace VnStyle.Web.Infrastructure.Security.Hash
{
    public class HashIds : IHashKey
    {
        private static Hashids hashids = new Hashids("BizBeauteSalt", 7);
        public string HashEncodeId(int pId)
        {
            return hashids.Encode((int)pId);
        }

        public int? HashDecodeId(string hashValue)
        {
            try
            {
                return hashids.Decode(hashValue).First();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string HashEncodeLongId(long pId)
        {
            return hashids.EncodeLong(pId);
        }

        public long? HashDecodeLongId(string hashValue)
        {
            try
            {
                return hashids.DecodeLong(hashValue).First();
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}