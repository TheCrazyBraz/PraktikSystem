using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PraktikSystem.Services;

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

        public IActionResult OnPost()
        {

            var user = _userService.getUser(Username);
            // Implement your authentication logic here
            // For simplicity, let's assume a hardcoded username and password
            if (user != null && user.Password == Password)
            {
                // Successful login, redirect to a dashboard or home page
                return RedirectToPage("/Privacy");
            }
            else if (user != null && user.Password != Password || user == null)
            {
                // Failed login, display an error
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            } else
            {
                return Page();
            }
        }
    }
}