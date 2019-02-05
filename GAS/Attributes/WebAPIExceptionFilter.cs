using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace GAS.Attributes
{
    public class WebAPIExceptionFilter : ExceptionFilterAttribute 
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is InvalidOperationException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.Exception.Message);
                    
            }
            else if (context.Exception is NotImplementedException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotImplemented, context.Exception.Message);
            }
            else
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, context.Exception.Message);

            }
        }
    }

}