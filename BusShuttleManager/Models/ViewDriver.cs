using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleManager.Models
{
    public class ViewDriver
    {
        public int Id {get; set;}

        public string FirstName {get; set;}

        public string LastName {get; set;}

        public static ViewDriver FromDriver(Driver driver)
        {
            return new ViewDriver
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName
            };
        }
    }
}