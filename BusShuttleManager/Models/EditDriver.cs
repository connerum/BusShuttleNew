using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleManager.Models
{
    public class EditDriver
    {
       
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        public string FirstName {get; set;}

        public string LastName {get; set;}


        public static EditDriver FromDriver(Driver driver)
        {
            return new EditDriver
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName
            };
        }
    }
}