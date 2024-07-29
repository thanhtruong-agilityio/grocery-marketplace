namespace GroceryMarketPlace.API.Validators.Product
{
    using Domain.Models;
    using FluentValidation;

    public class ProductQueryParametersValidator : AbstractValidator<ProductQueryParameters>
    {
        public ProductQueryParametersValidator()
        {
            RuleFor(u => u.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0");

            RuleFor(u => u.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0");
        }
    }
}
