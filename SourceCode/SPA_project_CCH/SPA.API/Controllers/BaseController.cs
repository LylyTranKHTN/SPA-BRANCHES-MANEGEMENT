using API.Model.HangdingCodeModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;

namespace SPA.API.Controllers
{
    public class BaseController : ApiController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        protected HttpResponseMessage CreateOkResponse<T>(MessageDataModel messageData, T data, int? pageNumber = null, 
                                                        int? pageSize = null, int? totalRecordCount = null)
        {
            return CreateResponse(HttpStatusCode.OK, null, messageData, data, pageNumber, pageSize, totalRecordCount);
        }

        protected HttpResponseMessage CreateNotFoundResponse(MessageDataModel messageData, string message)
        {
            var status = HttpStatusCode.NotFound;
            var errorResponse = new ErrorResponseModel
            {
                StatusCode = (int)status,
                StatusDescription = status.ToString(),
                Message = message,
                Validation = null
            };

            return CreateResponse<string>(status, errorResponse, messageData);
        }

        protected HttpResponseMessage CreateSystemErrorResponse(MessageDataModel messageData)
        {
            var status = HttpStatusCode.InternalServerError;
            var errorResponse = new ErrorResponseModel
            {
                StatusCode = (int)status,
                StatusDescription = status.ToString(),
                Message = "Server error",
                Validation = null,
            };

            return CreateResponse<string>(status, errorResponse, messageData);
        }

        protected HttpResponseMessage CreateValidationErrorResponse(MessageDataModel messageData, ValidationResult validationResult)
        {
            var status = HttpStatusCode.OK;
            var errorResponse = new ErrorResponseModel
            {
                StatusCode = (int)status,
                StatusDescription = status.ToString(),
                Message = "Validation Error",
                Validation = validationResult
            };

            return CreateResponse<string>(status, errorResponse, messageData);
        }

        protected HttpResponseMessage CreateResponse<T>(HttpStatusCode statusCode, ResponseModel<T> response)
        {
            var formatter = GetFormatter();
            var resp = new HttpResponseMessage(statusCode)
            {
                Content = new ObjectContent<ResponseModel<T>>(response, formatter)
            };

            return resp;
        }
        protected HttpResponseMessage CreateBadRequestErrorResponse(MessageDataModel messageData, string errorMessage)
        {
            var status = HttpStatusCode.BadRequest;
            var errorResponse = new ErrorResponseModel
            {
                StatusCode = (int)status,
                StatusDescription = status.ToString(),
                Message = errorMessage,
            };

            return CreateResponse<string>(status, errorResponse, messageData);
        }

        protected HttpResponseMessage CreateResponse<T>(HttpStatusCode statusCode, ErrorResponseModel error, MessageDataModel messageData)
        {
            var response = new ResponseModel<T>
            {
                Error = error,
                MessageDetails = messageData,
                Result = null
            };

            return CreateResponse(statusCode, response);
        }

        protected HttpResponseMessage CreateResponse<T>(HttpStatusCode statusCode, ErrorResponseModel error, MessageDataModel messageData,
                                                        T data, int? pageNumber = null, int? pageSize = null, int? totalRecordCount = null)
        {
            var response = new ResponseModel<T>
            {
                Error = error,
                MessageDetails = messageData,
                Result = new ResultResponseModel<T>
                {
                    Data = data,
                    Paging = null
                }
            };

            if (pageNumber.HasValue && pageSize.HasValue && totalRecordCount.HasValue)
            {
                response.Result.Paging = new PagingInfo()
                {
                    PageNumber = pageNumber.Value,
                    PageSize = pageSize.Value,
                    TotalRecordCount = totalRecordCount.Value,
                    TotalPageCount = totalRecordCount.Value > 0
                        ? (int)Math.Ceiling(totalRecordCount.Value / (double)pageSize.Value)
                        : 0
                };
            }

            return CreateResponse(statusCode, response);
        }

        protected MessageDataModel CreateMessageData(string route, params KeyValuePair<string, string>[] parameters)
        {
            var message = new MessageDataModel
            {
                Api = $"{route}",
                Parameters = parameters?.ToList()
            };
            return message;
        }

        public string GetRoute(string route, int? pageNumber, int? pageSize, string sort = null, string filter = null)
        {

            if (!pageNumber.HasValue || !pageSize.HasValue)
                return route;

            string result = route;

            result = $"{route}?pagenumber={pageNumber}&pagesize={pageSize}";

            if (!string.IsNullOrWhiteSpace(sort))
                result = result + "&sort=" + sort;

            if (!string.IsNullOrWhiteSpace(filter))
                result = result + "&filter" + filter;

            return result;
        }

        private JsonMediaTypeFormatter GetFormatter()
        {
            return new JsonMediaTypeFormatter()
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter>
                    {
                        new StringEnumConverter()
                    },
                    NullValueHandling = NullValueHandling.Ignore
                }
            };
        }
    }
}
