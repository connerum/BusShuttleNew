using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;

namespace BusShuttleManager.Models
{
    public class StopViewModel
    {
        public int Id { get; set;}

        public string Name {get; set;}      

        public double Latitude {get; set;}

        public double Longitude {get; set;}  

        public static StopViewModel FromStop(Stop stop)
        {
            return new StopViewModel
            {
                Id = stop.Id,
                Name = stop.Name,
                Latitude = stop.Latitude,
                Longitude = stop.Longitude
            };
        } 
    }
}