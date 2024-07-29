namespace GroceryMarketPlace.Domain.Enums
{
    public enum ErrorCode
    {
        // 400
        InvalidRequest,
        InsufficientStockQuantity,
        EmptyCart,

        // 401
        InvalidCredentials,

        // 403,
        Forbidden,

        // 404
        UserNotFound,
        ProductNotFound,
        CartNotFound,
        CartItemNotFound,
        OrderNotFound,

        // 409
        EmailExists,
        RoleExists,

        // 500
        AuthError,
        UserError,
        InternalServerError
    }
}
