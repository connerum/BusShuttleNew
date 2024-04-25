using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleManager.Models
{
    public class CreateBus
    {
       
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        public string BusName {get; set;}

        public static CreateBus NewBus(int amountOfBusses)
        {
            var newId = amountOfBusses + 1;

            return new CreateBus
            {
                Id = newId,
                BusName = "",
            };
        }
    }
}