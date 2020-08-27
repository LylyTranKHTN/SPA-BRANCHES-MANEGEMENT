using API.Model.HangdingCodeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace SPA.API.Handler
{
    public class ExceptionErrorHandler:ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            const string genericErrorMessage = "An unexpected error occured";

            var errorResponse = new ErrorResponseModel
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                StatusDescription = HttpStatusCode.InternalServerError.ToString(),
                Message = context.Exception.ToString(),
                Validation = null,
            };

            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                new
                {
                    Message = errorResponse
                });

            response.Headers.Add("X-Error", genericErrorMessage);
            context.Result = new ResponseMessageResult(response);

        }
    }
}