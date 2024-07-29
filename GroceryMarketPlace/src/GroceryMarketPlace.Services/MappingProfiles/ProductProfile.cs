namespace GroceryMarketPlace.Services.MappingProfiles
{
    using AutoMapper;
    using Domain.Dtos.Product;
    using Domain.Entities;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<AddProductRequestDto, Product>();
            this.CreateMap<Product, AddProductResponseDto>();
            this.CreateMap<Product, GetProductResponseDto>();
            this.CreateMap<UpdateProductRequestDto, Product>();
            this.CreateMap<Product, ProductDto>();
        }
    }
}
