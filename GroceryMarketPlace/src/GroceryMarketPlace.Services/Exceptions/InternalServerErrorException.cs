namespace GroceryMarketPlace.Services.Exceptions
{
    using Domain.Enums;

    public class InternalServerErrorException(string message, ErrorCode errorCode) : HttpException(message, errorCode)
    {
    }
}
