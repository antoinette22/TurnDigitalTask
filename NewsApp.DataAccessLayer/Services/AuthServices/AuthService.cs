using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using NewsApp.ApplicationLayer.Dtos.AuthDtos;
using NewsApp.ApplicationLayer.Interfaces;
using NewsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DataAccessLayer.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        

      
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    Succeeded = false,
                    ErrorMessage = "Invalid email or password."
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return new AuthResponseDto
                {
                    Succeeded = false,
                    ErrorMessage = "Invalid login attempt!"
                };




            await _signInManager.SignInAsync(user, isPersistent: false);

           
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            return new AuthResponseDto
            {
                Succeeded = true,
                IsAdmin = isAdmin
            };
        }



    }
}
