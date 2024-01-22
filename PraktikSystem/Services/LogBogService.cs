using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PraktikSystem.Services
{
    public class LogBogService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly List<Logbog> _logboge;

        public LogBogService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _logboge = new List<Logbog>();

            AddLogbog(new Logbog { Titel = "Test 1", Beskrivelse = "This is a test 1", Dato = DateTime.Now.AddDays(-5), ArchiveStatus = false, Username= "Test User" });
            AddLogbog(new Logbog { Titel = "Test 2", Beskrivelse = "This is a test 2", Dato = DateTime.Now.AddDays(-4), ArchiveStatus = false, Username = "Test User" });
            AddLogbog(new Logbog { Titel = "Test 3", Beskrivelse = "This is a test 3", Dato = DateTime.Now.AddDays(-3), ArchiveStatus = false, Username = "Test User" });
            AddLogbog(new Logbog { Titel = "Test 4", Beskrivelse = "This is a test 4", Dato = DateTime.Now.AddDays(-2), ArchiveStatus = false, Username = "Test User" });
            AddLogbog(new Logbog { Titel = "Test 5", Beskrivelse = "This is a test 5", Dato = DateTime.Now.AddDays(-1), ArchiveStatus = false, Username = "Test User" });

        }

        public void AddLogbog(Logbog logbog)
        {
            logbog.Id = _logboge.Count + 1;
            logbog.ArchiveStatus = false;

            // Access the current user's name from HttpContext
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;

            _logboge.Add(logbog);

            Console.WriteLine($"Added logbog: {logbog.Titel}, Count: {_logboge.Count}");
        }

        public List<Logbog> GetAllLogBoge()
        {
            return _logboge.ToList();
        }

        public Logbog GetLogbogById(int id)
        {
            return _logboge.FirstOrDefault(x => x.Id == id);
        }

        public void ArchiveLogbog(int id)
        {
            var logbog = _logboge.FirstOrDefault(x => x.Id == id);
            if (logbog != null)
            {
                logbog.ArchiveStatus = true;
            }
        }

        public void UpdateLogbog(Logbog updatedLogbog)
        {
            var existingLogbog = _logboge.FirstOrDefault(x => x.Id == updatedLogbog.Id);
            if (existingLogbog != null)
            {
                existingLogbog.Titel = updatedLogbog.Titel;
                existingLogbog.Beskrivelse = updatedLogbog.Beskrivelse;
                existingLogbog.Dato = updatedLogbog.Dato;
                // Update other properties as needed
            }
        }

        public void DeleteLogbog(int id)
        {
            var logbog = _logboge.FirstOrDefault(x => x.Id == id);
            if (logbog != null)
            {
                _logboge.Remove(logbog);
            }
        }


    }

    public class Logbog
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beskrivelse { get; set; }
        public double HoursDone { get; set; }
        public DateTime Dato { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool ArchiveStatus { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        [Display(Name = "Check-In Time")]
        [DataType(DataType.Time)]
        public DateTime? CheckInTime { get; set; }

        [Display(Name = "Check-Out Time")]
        [DataType(DataType.Time)]
        public DateTime? CheckOutTime { get; set; }
    }

}
