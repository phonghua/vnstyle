using System;
using System.Collections.Generic;
using System.Linq;

namespace VnStyle.Web.Infrastructure
{
    public class JsonDataResult
    {
        public dynamic Data { get; set; }
        public List<ErrorMessage> Errors { get; set; }
        public bool Success => !Errors.Any();
        public string Message { get; set; }
        public JsonDataResult()
        {
            Errors = new List<ErrorMessage>();
        }

        public void AddErrorMessage(ErrorMessage errorMessage)
        {
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));
            if (string.IsNullOrEmpty(errorMessage.FieldName)) errorMessage.FieldName = "";
            if (Errors.Exists(p => p.FieldName.Equals(errorMessage.FieldName, StringComparison.Ordinal)))
            {
                this.Errors.ForEach(p =>
                {
                    if (p.FieldName.Equals(errorMessage.FieldName, StringComparison.Ordinal))
                        p.Messages.AddRange(errorMessage.Messages);
                });
            }
            else
            {
                Errors.Add(errorMessage);
            }
        }

        public void AddErrorMessage(string message)
        {
            AddErrorMessage(new ErrorMessage { Messages = new List<string> { message } });
        }
    }
}