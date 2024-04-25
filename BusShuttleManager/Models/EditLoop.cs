using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleManager.Models
{
    public class EditLoop
    {
       
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        public string Name {get; set;}

        public static EditLoop FromLoop(Loop loop)
        {
            return new EditLoop
            {
                Id = loop.Id,
                Name = loop.Name,
            };
        }
    }
}