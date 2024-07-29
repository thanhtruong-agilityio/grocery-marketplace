namespace GroceryMarketPlace.Services.Exceptions
{
    using Domain.Enums;

    public class UnauthorizedException(string message, ErrorCode errorCode) : HttpException(message, errorCode)
    {
    }
}
