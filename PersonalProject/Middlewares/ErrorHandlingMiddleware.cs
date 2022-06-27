using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PersonalProject.Errors;
using PersonalProject.Response;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PersonalProject.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;


        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory) //ILogger logger
        {
            this.next = next;
            _logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
            //_logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.ToString());
            var code = HttpStatusCode.OK; // 500 if unexpected

            // if (ex is MyNotFoundException) code = HttpStatusCode.NotFound;
            //else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (ex is MyException) code = HttpStatusCode.BadRequest;

            var errorMsg = SetErrorKeywordByExType(ex);

            var result = JsonConvert.SerializeObject(
                new ApiResponse(errorMsg),  //new ApiResponse(ex.Message),
                 new JsonSerializerSettings
                 {
                     ContractResolver = new CamelCasePropertyNamesContractResolver()
                 });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private string SetErrorKeywordByExType(Exception ex) => ex is CustomError ? ex.Message : "system_error";
    }
}
