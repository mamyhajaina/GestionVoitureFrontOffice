using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GestionVoitureFrontOffice.Controllers
{
    [ApiController]
    [Route("admin/api/[controller]")]
    public class LoginAPIController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Ok("Test Swagger");
    }
}
