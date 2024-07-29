namespace GroceryMarketPlace.Services.Exceptions
{
    using Domain.Enums;

    public class ForbiddenException(string message, ErrorCode errorCode) : HttpException(message, errorCode)
    {
    }
}
