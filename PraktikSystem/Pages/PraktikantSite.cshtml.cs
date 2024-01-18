using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using PraktikSystem.Services;
using static PraktikSystem.Services.UserService;

namespace PraktikSystem.Pages
{
    public class PraktikantSiteModel : PageModel
    {

        public PraktikantSiteModel(LogBogService logBogService)
        {
            _logbogService = logBogService;
        }
        public void OnGet()
        {
            var username = User.Identity.Name;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            Console.WriteLine("OnGet method called in PraktikantSiteModel");
            Logboge = _logbogService.GetAllLogBoge();

        }

        private readonly LogBogService _logbogService;

        [BindProperty]
        public Logbog NyLogBog { get; set; }

        public List<Logbog> Logboge { get; set; }



        public IActionResult OnPostGodkend()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _logbogService.AddLogbog(NyLogBog);

            // Update user claims if necessary
            UpdateUserClaims();

            // Update the Model.Logboge collection
            Logboge = _logbogService.GetAllLogBoge();
            Console.WriteLine($"Logboge count after adding: {Logboge.Count}");


            // Reset the NyLogBog property for a new Logbog entry
            NyLogBog = new Logbog();

            return Page();
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

    }
}
