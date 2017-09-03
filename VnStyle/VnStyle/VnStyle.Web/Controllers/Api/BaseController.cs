using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using FluentValidation.Results;
using VnStyle.Web.Infrastructure;

namespace VnStyle.Web.Controllers.Api
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BaseController : ApiController
    {
        public JsonDataResult JsonDataResult { get; set; }

        public BaseController()
        {
            JsonDataResult = new JsonDataResult();
        }

        protected HttpResponseMessage CreateResponseMessage()
        {
            if (JsonDataResult.Success == false) return this.Request.CreateResponse(HttpStatusCode.BadRequest, JsonDataResult);
            return this.Request.CreateResponse(HttpStatusCode.OK, JsonDataResult);
        }

        protected HttpResponseMessage CreateBadRequestResponseMessage(ValidationResult validationResult)
        {
            if (validationResult.IsValid) { throw new Exception("The method CreateBadRequestResponseMessage only call if validation.IsValid is false"); }
            JsonDataResult.Errors.AddRange(validationResult.Errors
                .GroupBy(p => p.PropertyName)
                .Select(p => new ErrorMessage { FieldName = p.Key, Messages = p.Select(m => m.ErrorMessage).ToList() }));

            return this.Request.CreateResponse(HttpStatusCode.BadRequest, JsonDataResult);
        }
    }
}
