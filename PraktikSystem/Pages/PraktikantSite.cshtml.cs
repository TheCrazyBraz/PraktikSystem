using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using PraktikSystem.Services;

namespace PraktikSystem.Pages
{
    public class PraktikantSiteModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PraktikantSiteModel(IHttpContextAccessor httpContextAccessor, LogBogService logbogService)
        {
            _httpContextAccessor = httpContextAccessor;
            _logbogService = logbogService;
        }
        public void OnGet()
        {
            var username = User.Identity.Name;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            Console.WriteLine("OnGet method called in PraktikantSiteModel");
            Logboge = _logbogService.GetAllLogBoge();

            foreach (var logbog in Logboge)
            {
                logbog.HoursDone = CalculateHoursDone(logbog);
            }
        }

        private readonly LogBogService _logbogService;

        [BindProperty]
        public Logbog NyLogBog { get; set; }

        public List<Logbog> Logboge { get; set; }



        public IActionResult OnPostGodkend()
        {
            try
            {
                Console.WriteLine("OnPostGodkend method called");

                // Validate the model state
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("Model state is not valid");
                    return Page();
                }

                // Pass the logbog to the service method
                _logbogService.AddLogbog(NyLogBog);

                // Update user claims if necessary
                UpdateUserClaims();

                // Update the Model.Logboge collection
                Logboge = _logbogService.GetAllLogBoge();
                Console.WriteLine($"Logboge count after adding: {Logboge.Count}");

                // Reset the NyLogBog property for a new Logbog entry
                NyLogBog = new Logbog();

                Console.WriteLine("OnPostGodkend method completed successfully");
                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in OnPostGodkend: {ex.Message}");

                // Print model state errors
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Model state error for {key}: {error.ErrorMessage}");
                    }
                }

                return Page();
            }
        }

        public IActionResult OnGetAddLogbog()
        {
            NyLogBog = new Logbog(); // Initialize a new Logbog
            return RedirectToPage("/Logbog_displays/AddLogbog", new { id = NyLogBog.Id });
        }

        public IActionResult OnGetLogbogDetails(int id)
        {
            // Redirect to the EditLogbog page with the selected Logbog ID
            return RedirectToPage("/Logbog_displays/LogbogDetails", new { id });
        }

        private void UpdateUserClaims()
        {
            // Perform logic to update user claims if necessary
        }

        public IActionResult OnPostArchive(int id)
        {
            _logbogService.ArchiveLogbog(id);
            Logboge = _logbogService.GetAllLogBoge();

            return Page();

        }

        public double CalculateHoursDone(Logbog logbog)
        {
            if (logbog.StartTime != null && logbog.EndTime != null)
            {
                TimeSpan timeDifference = logbog.EndTime - logbog.StartTime;

                logbog.HoursDone = (double)timeDifference.TotalHours;

                return logbog.HoursDone;
            }

            return 0;
        }

    }
}
