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
        }

        public void AddLogbog(Logbog logbog)
        {
            logbog.Id = _logboge.Count + 1;
            _logboge.Add(logbog);
        }

        public List<Logbog> GetAllLogBoge()
        {
            return _logboge.ToList();
        }

        public Logbog GetLogbogById(int id)
        {
            return _logboge.Find(x => x.Id == id);
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
