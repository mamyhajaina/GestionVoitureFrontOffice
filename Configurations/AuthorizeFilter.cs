using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GestionVoitureFrontOffice.Configurations
{
    public class AuthorizeFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("OnActionExecuting");
            var userEmail = context.HttpContext.Items["UserEmail"]?.ToString();
            var userRole = context.HttpContext.Items["UserRole"]?.ToString();
            var actionName = context.ActionDescriptor.RouteValues["action"];
            Console.WriteLine("userEmail: " + userEmail);
            Console.WriteLine("userRole: " + userRole);
            Console.WriteLine("actionName: " + actionName);
            if (actionName == "Index")
            {
                Console.WriteLine("actionName Index");
                return;
            }
            if (string.IsNullOrEmpty(userEmail))
            {
                Console.WriteLine("RedirectToActionResult Login");
                if (string.Equals(context.HttpContext.Request.Method, "POST", StringComparison.OrdinalIgnoreCase)) {

                    Console.WriteLine("Post");
                    context.HttpContext.Response.StatusCode = 200 ;
                    context.Result = new ContentResult
                    {
                        Content = "<script>window.location.href='/Login/Index';</script>",
                        ContentType = "text/html"
                    };
                }
                else
                {
                    Console.WriteLine("Get");
                    context.Result = new RedirectToActionResult("Index", "Login", null);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Rien à faire après l'exécution
        }
    }
}
