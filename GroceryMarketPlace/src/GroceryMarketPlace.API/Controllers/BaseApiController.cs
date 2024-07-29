namespace GroceryMarketPlace.API.Controllers
{
    using Asp.Versioning;
    using Filters;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiActionFilter]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public string UserId { get; set; } = string.Empty;
    }
}
