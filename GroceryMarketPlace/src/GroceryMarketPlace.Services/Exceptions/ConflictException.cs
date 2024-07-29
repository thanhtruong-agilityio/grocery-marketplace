namespace GroceryMarketPlace.Services.Exceptions
{
    using Domain.Enums;

    public class ConflictException(string message, ErrorCode errorCode) : HttpException(message, errorCode)
    {
    }
}
