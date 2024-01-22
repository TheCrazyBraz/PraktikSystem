using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PraktikSystem.Services;

namespace PraktikSystem.Pages.CRUD_Operations
{
    public class DeleteLogbogModel : PageModel
    {
        private readonly LogBogService _logBogService;

        public DeleteLogbogModel(LogBogService logBogService)
        {
            _logBogService = logBogService;
        }

        public Logbog LogbogToDelete { get; set; }

        public IActionResult OnGet(int id)
        {
            // Handle GET request and retrieve logbog for deletion
            LogbogToDelete = _logBogService.GetLogbogById(id);

            if (LogbogToDelete == null)
            {
                return RedirectToPage("/Index"); // Redirect if logbog not found
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            // Handle POST request and delete logbog
            _logBogService.DeleteLogbog(LogbogToDelete.Id);

            return RedirectToPage("/Index"); // Redirect to the index page or another page
        }
    }
}