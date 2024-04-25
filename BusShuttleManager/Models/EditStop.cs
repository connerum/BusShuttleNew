using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleManager.Models
{
    public class EditStop
    {
       
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        public string Name {get; set;}


        public static EditStop FromStop(Stop stop)
        {
            return new EditStop
            {
                Id = stop.Id,
                Name = stop.Name,
            };
        }
    }
}