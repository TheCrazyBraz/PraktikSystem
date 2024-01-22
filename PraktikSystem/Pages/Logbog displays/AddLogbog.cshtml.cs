using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PraktikSystem.Services;

namespace PraktikSystem.Pages.Logbog_displays
{
    public class AddLogbogModel : PageModel
    {
        private readonly LogBogService _logBogService;

        public AddLogbogModel(LogBogService logBogService)
        {
            _logBogService = logBogService;
        }

        [BindProperty]
        public Logbog NewLogbog { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _logBogService.AddLogbog(NewLogbog);

            return RedirectToPage("/PraktikantSite");
        }
    }
}
