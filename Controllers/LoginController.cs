﻿using GestionVoitureFrontOffice.Models;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(UserService userService, JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                _httpContextAccessor.HttpContext?.LogoutAsync();
                Console.WriteLine("Login");
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ViewData["ErrorMessage"] = "L'email et le mot de passe sont requis.";
                    Console.WriteLine("L'email et le mot de passe sont requis.");
                    return RedirectToAction("Index", "Login");
                }

                var emailIsValid = new EmailAddressAttribute().IsValid(email);
                if (!emailIsValid)
                {
                    ViewData["ErrorMessage"] = "L'email fourni n'est pas valide.";
                    Console.WriteLine("L'email fourni n'est pas valide.");
                    return RedirectToAction("Index", "Login");
                }

                var user = await _userService.LoginAsync(email, password);

                if (user != null)
                {
                    Console.WriteLine("user.RoleUser.Nam == " + user.RoleUser.Name);
                    if (user.RoleUser.Name == "Client")
                    {
                        var token = _jwtTokenService.GenerateToken(user.Id, email, user.RoleUser.Name);
                        HttpContext.Session.SetString("JwtToken", token);
                        Console.WriteLine("token == " + token);
                        return RedirectToAction("Index", "AdminClient");
                    }
                    else if(user.RoleUser.Name == "Admin")
                    {
                        var token = _jwtTokenService.GenerateToken(user.Id, email, user.RoleUser.Name);
                        HttpContext.Session.SetString("JwtToken", token);
                        Console.WriteLine("Admin");
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        Console.WriteLine("Else");
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    Console.WriteLine("Email ou mot de passe incorrect");
                    ViewData["ErrorMessage"] = "Email ou mot de passe incorrect";
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de la tentative de connexion : " + ex.Message);
                ViewData["ErrorMessage"] = "Une erreur s'est produite lors de la tentative de connexion. Veuillez réessayer plus tard.";

                return StatusCode(500, new
                {
                    message = "Une erreur interne est survenue. Veuillez réessayer plus tard.",
                    details = ex.Message // Inclure d'autres détails si nécessaire
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("FirstName,LastName,Email,PasswordHash")] User user)
        {
            Console.WriteLine("user" + user);
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
                        return RedirectToAction("Index", "Login");

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Échec de l'inscription.";
                        return RedirectToAction("Register", "Login");

                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "L'utilisateur existe déjà.";
                    return RedirectToAction("Register", "Login");
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

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.LogoutAsync();
        }
    }
}
