namespace GroceryMarketPlace.Services.Exceptions
{
    using Domain.Enums;

    public class BadRequestException(string message, ErrorCode errorCode) : HttpException(message, errorCode)
    {
    }
}
