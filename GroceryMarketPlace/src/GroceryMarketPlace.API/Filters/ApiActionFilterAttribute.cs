namespace GroceryMarketPlace.API.Filters
{
    using System.Security.Claims;
    using Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;

    [AttributeUsage(AttributeTargets.All)]
    public class ApiActionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Pull the user ID on each request
            var controller = context.Controller as BaseApiController;
            controller!.UserId = controller.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
