using System;

namespace Ricky.Infrastructure.Core.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CubeDateDimentionAttribute : Attribute
    {
        public string OriginalProperty { get; set; }
    }
}