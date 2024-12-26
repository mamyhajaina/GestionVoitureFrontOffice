using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionVoitureFrontOffice.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;
        private readonly JwtTokenService _jwtTokenService;

        public LoginController(UserService userService, JwtTokenService jwtTokenService) {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ViewData["ErrorMessage"] = "L'email et le mot de passe sont requis.";
                    Console.WriteLine("L'email et le mot de passe sont requis.");
                    return RedirectToAction("");
                }

                var emailIsValid = new EmailAddressAttribute().IsValid(email);
                if (!emailIsValid)
                {
                    ViewData["ErrorMessage"] = "L'email fourni n'est pas valide.";
                    Console.WriteLine("L'email fourni n'est pas valide.");
                    return RedirectToAction("");
                }

                var user = await _userService.LoginAsync(email, password);

                if (user != null)
                {
                    Console.WriteLine("user.RoleUser.Nam == " + user.RoleUser.Name);
                    if (user.RoleUser.Name == "Client") {
                        var token = _jwtTokenService.GenerateToken(email, user.RoleUser.Name);
                        HttpContext.Session.SetString("JwtToken", token);
                        Console.WriteLine("token == " + token);
                        return RedirectToAction("Index", "Vehicle");
                    }
                    else {
                        Console.WriteLine("No Token");
                        return RedirectToAction("");
                    }
                }
                else
                {
                    Console.WriteLine("Email ou mot de passe incorrect");
                    ViewData["ErrorMessage"] = "Email ou mot de passe incorrect";
                    return RedirectToAction("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de la tentative de connexion. Veuillez réessayer plus tard.", ex);
                ViewData["ErrorMessage"] = "Une erreur s'est produite lors de la tentative de connexion. Veuillez réessayer plus tard.";
                return Unauthorized(new { message = "Données invalides. Veuillez vérifier les champs.", ex });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("FirstName,LastName,Email,PasswordHash")] User user)
        {
            Console.WriteLine("user"+ user);
            user.RoleId = 2;
            if (ModelState.IsValid)
            {
                var checkUser = await _userService.CheckUserAsync(user.Email);

                if (checkUser)
                {
                    var result = await _userService.AddUser(user);
                    if (result)
                    {
                        TempData["Message"] = "Inscription réussie.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Échec de l'inscription.";
                        return RedirectToAction("Register");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "L'utilisateur existe déjà.";
                    return RedirectToAction("Register");
                }
            }
            else
            {
                //var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                //Console.WriteLine("Erreurs de validation : " + string.Join(", ", errors));
                //TempData["ErrorMessage"] = "Données invalides. Veuillez vérifier les champs.";
                //return Unauthorized(new { message = "Données invalides. Veuillez vérifier les champs.", errors });
                
                TempData["ErrorMessage"] = "Données invalides. Veuillez vérifier les champs.";
                //return RedirectToAction("Register");
                return Unauthorized(new { message = "Données invalides. Veuillez vérifier les champs.", user });
            }

        }


    }
}
