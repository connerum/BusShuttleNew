using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;

namespace BusShuttleManager.Models
{
    public class ViewEntry
    {
        public int Id { get; set;}

        public DateTime TimeStamp {get; set;}      

        public int Boarded {get; set;}

        public int LeftBehind {get; set;}  

        public string LoopName { get; set; }

        public string DriverName { get; set; }

        public string StopName { get; set; }

        public string BusName { get; set; }

        public static ViewEntry FromEntry(Entry entry, Loop loop, Driver driver, Stop stop, Bus bus)
        {
            return new ViewEntry
            {
                Id = entry.Id,
                TimeStamp = entry.TimeStamp,
                Boarded = entry.Boarded,
                LeftBehind = entry.LeftBehind,
                LoopName = loop.Name,
                DriverName = driver.FirstName,
                StopName = stop.Name,
                BusName = bus.BusName
            };
        }
    }
}