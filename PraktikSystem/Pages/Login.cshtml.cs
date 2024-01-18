using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PraktikSystem.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PraktikSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
            // Handle GET request
        }

        public async Task<IActionResult> OnPost()
        {
            var user = _userService.getUser(Username);

            if (user != null && user.Password == Password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    // Add other claims as needed
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // You can add additional properties if needed
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                if (user.Role == "Praktikant")
                {
                    return RedirectToPage("/PraktikantSite");
                }
                else if (user.Role == "Admin")
                {
                    return RedirectToPage("/AdminSite");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return Page();
        }
    }
}
