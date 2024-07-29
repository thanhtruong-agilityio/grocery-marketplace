namespace GroceryMarketPlace.Domain.Dtos;
public class ExceptionResponseDto
{
    public int StatusCode { get; set; }
    public required string ErrorCode { get; set; }
    public required string Message { get; set; }
}
