using System;

namespace Ricky.Infrastructure.Core.Exceptions
{
    public class BadInfoException : Exception
    {
        public BadInfoException(string message) : base(message) { }
    }
}
