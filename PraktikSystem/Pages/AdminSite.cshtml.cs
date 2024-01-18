using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PraktikSystem.Services;

namespace PraktikSystem.Pages
{
    public class AdminSiteModel : PageModel
    {
        private readonly LogBogService _logBogService;

        public AdminSiteModel(LogBogService logBogService)
        {
            _logBogService = logBogService;
        }

        public void OnGet()
        {
            string sortOrder = Request.Query["sortorder"];
            // Retrieve all logbogs
            var logbogs = _logBogService.GetAllLogBoge();
            if (sortOrder == "desc")
            {
                logbogs = logbogs.OrderByDescending(l => l.Id).ToList();
            }
            else
            {
                logbogs = logbogs.OrderBy(l => l.Id).ToList();
            }
        }
        public IActionResult OnGetSort(string sortOrder)
        {
            // Retrieve all logbogs
            var logbogs = _logBogService.GetAllLogBoge();

            Console.WriteLine($"Sorting order: {sortOrder}");
            Console.WriteLine($"Number of logbogs Before sorting: {logbogs?.Count}");

            // Sort logbogs based on the sortOrder parameter
            if (sortOrder == "desc")
            {
                logbogs = logbogs.OrderByDescending(l => l.Id).ToList();
            }
            else
            {
                logbogs = logbogs.OrderBy(l => l.Id).ToList();
            }

            Console.WriteLine($"Number of logbogs after sorting: {logbogs?.Count}");

            // Return a partial view with the sorted logbogs
            return Partial("_LogbogsPartial", logbogs);
        }
        public void OnPostArchive(int id)
        {
            // Archive logbog
            _logBogService.ArchiveLogbog(id);

            // Refresh the logbogs list after archiving
            Logbogs = _logBogService.GetAllLogBoge();
        }


        public List<Logbog> Logbogs { get; set; }
    }
}
