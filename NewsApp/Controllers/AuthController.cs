using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsApp.ApplicationLayer.Dtos.AuthDtos;
using NewsApp.ApplicationLayer.Interfaces;

namespace NewsApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);

            var result = await _authService.LoginAsync(loginDto);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(loginDto);
            }

            if (result.IsAdmin)
                return RedirectToAction("Index", "Dashboard");

            return RedirectToAction("Index", "Home");
        }

       

    }
}
