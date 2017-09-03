using System.Collections.Generic;

namespace VnStyle.Web.Infrastructure
{
    public class ErrorMessage
    {
        public string FieldName { get; set; }
        public List<string> Messages { get; set; }
    }
}