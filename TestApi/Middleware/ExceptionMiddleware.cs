using System.Net;
using System.Text.Json;
using TestApi.Helpers;

namespace TestApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new ApiResponse<string>(false, ex.Message, null);
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }

}
