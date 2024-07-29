namespace GroceryMarketPlace.API.Controllers
{
    using Domain.Dtos.Product;
    using Domain.Interfaces.Services;
    using Domain.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController(IProductService productService) : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<GetAllProductsResponseDto>> GetAll([FromQuery] ProductQueryParameters queryParameters)
        {
            var result = await productService.GetAllAsync(queryParameters);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AddProductResponseDto>> Create([FromBody] AddProductRequestDto requestDTO)
        {
            var result = await productService.AddAsync(requestDTO);

            return CreatedAtAction(nameof(this.Create), result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<GetProductResponseDto>> GetById([FromRoute] string id)
        {
            var result = await productService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteById([FromRoute] string id)
        {
            await productService.DeleteByIdAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateById([FromRoute] string id, [FromBody] UpdateProductRequestDto requestDTO)
        {
            await productService.UpdateByIdAsync(id, requestDTO);

            return NoContent();
        }
    }
}
