namespace GroceryMarketPlace.Services.Exceptions
{
    using Domain.Enums;

    public class NotFoundException(string message, ErrorCode errorCode) : HttpException(message, errorCode)
    {
    }
}
