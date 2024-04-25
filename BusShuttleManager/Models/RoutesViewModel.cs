using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;

namespace BusShuttleManager.Models
{
    public class RoutesViewModel
    {
        public List<Loop> Loops { get; set; }
        public List<Routes> Routes { get; set; } 
        public Loop SelectedLoop { get; set; } 

        public List<Stop> Stops { get; set; }

        public static RoutesViewModel FromRoutes(List<Routes> routes, List<Loop> loops)
        {
            return new RoutesViewModel
            {
                Loops = loops,
                Routes = routes

            };
        } 

        public static RoutesViewModel FromLoopID(List<Routes> routes, List<Loop> loops, Loop selectedLoop, List<Stop> stops)
        {
            return new RoutesViewModel
            {
                Loops = loops,
                Routes = routes,
                SelectedLoop = selectedLoop,
                Stops = stops
            };
        }
    }
}