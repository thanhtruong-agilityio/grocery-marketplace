namespace GroceryMarketPlace.API.Filters;

using Domain.Dtos;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidationFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var message = context.ModelState.Values
                .SelectMany(modelState => modelState.Errors)
                .Select(modelError => modelError.ErrorMessage)
                .First();

            var result = new ExceptionResponseDto
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = message,
                ErrorCode = ErrorCode.InvalidRequest.ToString()
            };

            context.Result = new BadRequestObjectResult(result);
        }

        await next();
    }
}

