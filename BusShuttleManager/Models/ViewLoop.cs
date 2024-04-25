using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleManager.Models
{
    public class LoopViewModel
    {
        public int Id { get; set;}

        public string Name {get; set;}        

        public static LoopViewModel FromLoop(Loop loop)
        {
            return new LoopViewModel
            {
                Id = loop.Id,
                Name = loop.Name
            };
        } 
    }
}

