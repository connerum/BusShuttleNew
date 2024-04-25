using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleManager.Models
{
    public class EditBus
    {
       
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        public string BusName {get; set;}


        public static EditBus FromBus(Bus bus)
        {
            return new EditBus
            {
                Id = bus.Id,
                BusName = bus.BusName,
            };
        }
    }
}