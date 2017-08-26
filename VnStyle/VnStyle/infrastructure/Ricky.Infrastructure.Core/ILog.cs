using System;

namespace Ricky.Infrastructure.Core
{
    public interface ILog
    {

        void Error(object message, Exception exception = null, Type type = null);
        void Info(object message, Exception exception = null, Type type = null);

    }
}
