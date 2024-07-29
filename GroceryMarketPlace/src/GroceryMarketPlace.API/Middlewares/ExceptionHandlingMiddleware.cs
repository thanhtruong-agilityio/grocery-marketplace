namespace GroceryMarketPlace.API.Middlewares
{
    using System.Text.Json;
    using Domain.Dtos;
    using Domain.Enums;
    using Microsoft.AspNetCore.Http;
    using Services.Exceptions;

    public class ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger
    )
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (HttpException ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, new InternalServerErrorException(ex.Message, ErrorCode.InternalServerError));
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpException ex)
        {
            logger.LogError(ex, "An error occurred");

            var errorCode = ex.ErrorCode;
            var statusCode = StatusCodes.Status500InternalServerError;
            statusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                ConflictException => StatusCodes.Status409Conflict,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                InternalServerErrorException => StatusCodes.Status500InternalServerError,
                _ => statusCode
            };

            var result = JsonSerializer.Serialize(new ExceptionResponseDto
            {
                StatusCode = statusCode,
                Message = ex.Message,
                ErrorCode = errorCode.ToString()
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(result);
        }
    }
}
