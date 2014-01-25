using System;
using System.Data.Entity.Core;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Mundialito.Filters
{
    public class MundialitoExceptionFilterAttribute : ExceptionFilterAttribute 
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var status = HttpStatusCode.InternalServerError;
            if (context.Exception is NotImplementedException)
            {
                status = HttpStatusCode.NotImplemented;
            } else if (context.Exception is ObjectNotFoundException)
            {
                status = HttpStatusCode.NotFound;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                status = HttpStatusCode.Forbidden;
            }
            else if (context.Exception is ArgumentException)
            {
                status = HttpStatusCode.BadRequest;
            }

            context.Response = context.Request.CreateErrorResponse(status, context.Exception);
        }

        
    }
}