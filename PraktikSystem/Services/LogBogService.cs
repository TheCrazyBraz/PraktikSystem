using System;
using System.Collections.Generic;
using System.Linq;


namespace PraktikSystem.Services
{
    public class LogBogService
    {
        private readonly List<Logbog> _logboge;
        
        public LogBogService()
        {
            _logboge = new List<Logbog>();

            AddLogbog(new Logbog { Titel = "Test 1", Beskrivelse = "This is a test 1", Dato = DateTime.Now.AddDays(-5), ArchiveStatus = false });
            AddLogbog(new Logbog { Titel = "Test 2", Beskrivelse = "This is a test 2", Dato = DateTime.Now.AddDays(-4), ArchiveStatus = false });
            AddLogbog(new Logbog { Titel = "Test 3", Beskrivelse = "This is a test 3", Dato = DateTime.Now.AddDays(-3), ArchiveStatus = false });
            AddLogbog(new Logbog { Titel = "Test 4", Beskrivelse = "This is a test 4", Dato = DateTime.Now.AddDays(-2), ArchiveStatus = false });
            AddLogbog(new Logbog { Titel = "Test 5", Beskrivelse = "This is a test 5", Dato = DateTime.Now.AddDays(-1), ArchiveStatus = false });

        }

        public void AddLogbog(Logbog logbog)
        {
            logbog.Id = _logboge.Count + 1;
            logbog.ArchiveStatus = false;
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

        

    }

    public class Logbog
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beskrivelse { get; set; }
        public DateTime Dato { get; set; }
        public bool ArchiveStatus { get; set; }
    }
}
