namespace GroceryMarketPlace.API.Middlewares
{
    using Microsoft.AspNetCore.Http;

    public class RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            // Log request
            logger.LogInformation(
                "[{Time}] Request: {Method} {Url}",
                DateTime.Now.ToString(),
                context.Request?.Method,
                context.Request?.Path.Value);

            await next(context);

            // Log response
            logger.LogInformation(
                "[{Time}] Response: {Method} {Url} - {StatusCode}",
                DateTime.Now.ToString(),
                context.Request?.Method,
                context.Request?.Path.Value,
                context.Response?.StatusCode);
        }
    }
}
