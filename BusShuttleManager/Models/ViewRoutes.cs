using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;

namespace BusShuttleManager.Models
{
    public class ViewRoutes
    {
        public List<Loop> Loops { get; set; }
        public List<Routes> Routes { get; set; } 
        public Loop SelectedLoop { get; set; } 

        public List<Stop> Stops { get; set; }

        public static ViewRoutes FromRoutes(List<Routes> routes, List<Loop> loops)
        {
            return new ViewRoutes
            {
                Loops = loops,
                Routes = routes

            };
        } 

        public static ViewRoutes FromLoopID(List<Routes> routes, List<Loop> loops, Loop selectedLoop, List<Stop> stops)
        {
            return new ViewRoutes
            {
                Loops = loops,
                Routes = routes,
                SelectedLoop = selectedLoop,
                Stops = stops
            };
        }
    }
}