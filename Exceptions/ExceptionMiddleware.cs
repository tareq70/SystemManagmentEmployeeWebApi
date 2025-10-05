using System.Net;
using System.Text.Json;

namespace SystemManagmentEmployeeWebApi.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode status;
            string message = ex.Message;

            switch (ex)
            {
                case NotFoundException:
                    status = HttpStatusCode.NotFound;
                    break;
                case ArgumentException:
                    status = HttpStatusCode.BadRequest;
                    break;
                default:
                    status = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                error = message,
                statusCode = (int)status
            });

            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(result);
        }
    }
}