using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PraktikSystem.Services;

namespace PraktikSystem.Pages.CRUD_Operations
{
    public class UpdateLogbogModel : PageModel
    {
        private readonly LogBogService _logBogService;

        public UpdateLogbogModel(LogBogService logBogService)
        {
            _logBogService = logBogService;
        }

        [BindProperty]
        public Logbog UpdatedLogbog { get; set; }

        public IActionResult OnGet(int id)
        {
            // Handle GET request and retrieve logbog for updating
            UpdatedLogbog = _logBogService.GetLogbogById(id);

            if (UpdatedLogbog == null)
            {
                return RedirectToPage("/Index"); // Redirect if logbog not found
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            // Handle POST request and update logbog
            _logBogService.UpdateLogbog(UpdatedLogbog);

            return RedirectToPage("/Index");
        }
    }
}
