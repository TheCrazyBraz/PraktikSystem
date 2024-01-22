using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PraktikSystem.Services;

namespace PraktikSystem.Pages.CRUD_Operations
{
    public class CreateLogbogModel : PageModel
    {
        private readonly LogBogService _logBogService;

        public CreateLogbogModel(LogBogService logBogService)
        {
            _logBogService = logBogService;
        }

        public void OnGet()
        {
            // Handle GET request
        }

        public IActionResult OnPost()
        {
            // Handle POST request and create new logbog
            var newLogbog = new Logbog
            {
                // Initialize logbog properties from form data
            };

            _logBogService.AddLogbog(newLogbog);

            return RedirectToPage("/Index"); // Redirect to the index page or another page
        }
    }
}
