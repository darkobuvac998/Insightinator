using Newtonsoft.Json;
using System.Net;

namespace Insightinator.API.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            Message = "An error occured during the request.",
            Exception = exception.Message
        };

        var json = JsonConvert.SerializeObject(
            errorResponse,
            new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
        );

        await context.Response.WriteAsync(json);
    }

    private sealed class ErrorResponse
    {
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
