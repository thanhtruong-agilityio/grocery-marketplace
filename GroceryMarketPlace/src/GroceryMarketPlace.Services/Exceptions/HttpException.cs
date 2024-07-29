namespace GroceryMarketPlace.Services.Exceptions
{
    using Domain.Enums;

    public class HttpException(string message, ErrorCode errorCode) : Exception(message)
    {
        public ErrorCode ErrorCode = errorCode;
    }
}
