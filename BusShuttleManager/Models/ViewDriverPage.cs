using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;

namespace BusShuttleManager.Models
{
    public class ViewDriverPage
    {
        public List<Loop> Loops { get; set; }
        public List<Bus> Busses { get; set; }
        public List<Driver> Drivers { get; set; }

        public static ViewDriverPage FromData(List<Loop> loops, List<Bus> busses, List<Driver> drivers)
        {
            return new ViewDriverPage
            {
                Loops = loops,
                Busses = busses,
                Drivers = drivers
            };
        }
    }
}
