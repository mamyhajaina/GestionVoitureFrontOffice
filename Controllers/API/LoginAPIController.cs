using Microsoft.AspNetCore.Mvc;

namespace GestionVoitureFrontOffice.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginAPIController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Ok("Test Swagger");
    }
}
