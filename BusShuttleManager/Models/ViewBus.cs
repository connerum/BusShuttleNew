using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleManager.Models
{
    public class ViewBus
    {
        public int Id { get; set;}

        public string BusName {get; set;}        

        public static ViewBus FromBus(Bus bus)
        {
            return new ViewBus
            {
                Id = bus.Id,
                BusName = bus.BusName
            };
        } 
    }
}

