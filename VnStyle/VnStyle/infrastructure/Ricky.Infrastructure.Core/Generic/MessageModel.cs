using System.Collections.Generic;

namespace Ricky.Infrastructure.Core.Generic
{

    public class BaseResponse
    {
        public string Message { get; set; }
        public List<ResponseError> Errors { get; set; }

        public BaseResponse()
        {
            Errors = new List<ResponseError>();
        }
    }

    public class ResponseError
    {
        public string FieldErrorCode { get; set; }
        public string FieldName { get; set; }
        public string FieldMessage { get; set; }
    }
}
