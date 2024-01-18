using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System;
using System.Collections.Generic;

using PraktikSystem.Services;

namespace PraktikSystem.Pages
{
    public class PraktikantSiteModel : PageModel
    {
        public void OnGet()
        {
            var username = User.Identity.Name;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
        }

        private readonly LogBogService _logbogService;

    }
}
