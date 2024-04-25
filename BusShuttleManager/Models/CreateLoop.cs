using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleManager.Models
{
    public class CreateLoop
    {
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        public string Name {get; set;}

        public static CreateLoop NewLoop(int amountOfLoops)
        {
            var newId = amountOfLoops + 1;

            return new CreateLoop
            {
                Id = newId,
                Name = "",
            };
        }
    }
}