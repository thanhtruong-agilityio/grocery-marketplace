namespace GroceryMarketPlace.API.Validators.Product
{
    using Domain.Dtos.Product;
    using FluentValidation;

    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequestDto>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(u => u.StockQuantity)
                .GreaterThan(0).WithMessage("StockQuantity must be greater than 0");

            RuleFor(u => u.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
