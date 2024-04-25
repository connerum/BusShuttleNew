using System.ComponentModel.DataAnnotations;
namespace DomainModel
{
    public class Driver
    {
        [Key]
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public int Id {get; set;}
        public bool IsActive { get; set; }

        public Driver(int id, string fName, string lName)
        {
            Id = id;
            FirstName = fName;
            LastName = lName;
        }

        public Driver(Driver driver)
        {
            Id = driver.Id;
            FirstName = driver.FirstName;
            LastName = driver.LastName;
        }
        
        public Driver()
        {
            IsActive = true;
        }

        public void Update(string fName, string lName)
        {
            FirstName = fName;
            LastName = lName;
        }
    }
}

