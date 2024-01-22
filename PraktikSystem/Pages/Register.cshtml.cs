using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PraktikSystem.Services;

namespace PraktikSystem.Pages
{

    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_userService.getUser(Username) != null)
            {
                ModelState.AddModelError(nameof(Username), "Username already exists");
                return Page();
            }
            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError(nameof(ConfirmPassword), "Passwords does not match.- Please try again.");
                return Page();
            }
            _userService.AddUser(new UserService.User { Username = Username, Password = Password });

            return RedirectToPage("/Index");
        }
    }
}
