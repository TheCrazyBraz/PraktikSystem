using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PraktikSystem.Services;

namespace PraktikSystem.Pages.Logbog_displays
{
    public class LogbogDetailsModel : PageModel
    {
        private readonly LogBogService _logBogService;

        public LogbogDetailsModel(LogBogService logBogService)
        {
            _logBogService = logBogService;
        }

        [BindProperty]
        public Logbog Logbog { get; set; }

        public IActionResult OnGet(int id)
        {
            Logbog = _logBogService.GetLogbogById(id);

            if (Logbog == null)
            {
                // Handle the case where the log entry is not found (redirect or display an error)
                // For now, redirect to the main log page
                return RedirectToPage("/PraktikantSite");
            }

            return Page();
        }

        public IActionResult OnPostSubmit()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Logbog.CheckInTime.HasValue && Logbog.CheckOutTime.HasValue)
            {
                Logbog.HoursDone = (Logbog.CheckOutTime - Logbog.CheckInTime).Value.TotalHours;
            }

            _logBogService.UpdateLogbog(Logbog);

            return RedirectToPage("/PraktikantSite");
        }
    }
}
