using DomainModel;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BusShuttleManager.Data;
namespace BusShuttleManager.Services
{
    public class DriverService : IDriverService
    {
        private readonly ILogger<DriverService> logger;
        private DataContext db;
        List<Driver> drivers;

        private readonly AppDbContext _appDb;

        public DriverService(AppDbContext appDb, ILogger<DriverService> logger)
        {
            _appDb = appDb;
            this.logger = logger;
        }
        
        public void CreateNewDriver(int id, string fName, string lName, string email)
        {
            logger.LogInformation("Creating new driver with ID: {Id}", id);
            db = new DataContext();
            db.Add(new Driver{Id=id, FirstName=fName, LastName=lName, Email=email});
            db.SaveChanges();
        }
        
        public void deactivateDriver(int id)
        {
            logger.LogInformation("Deactivating driver with ID: {Id}", id);
            db = new DataContext();
            var existingDriver = db.Driver.FirstOrDefault(d => d.Id == id);
        
            if (existingDriver != null)
            {
                existingDriver.IsActive = false;
                db.SaveChanges();
            }
        }
        
        public void UpdateDriverById(int id, string fName, string lName)
        {
            logger.LogInformation("Updating driver with ID: {Id}", id);
            db = new DataContext();
            
            var existingDriver = db.Driver.SingleOrDefault(driver => driver.Id == id);
            existingDriver.Update(fName, lName);

            var driver = db.Driver
                .SingleOrDefault(driver => driver.Id == existingDriver.Id);
            
            if(driver != null)
            {
                driver.FirstName = fName;
                driver.LastName = lName;
                db.SaveChanges();
            }
        }
        
        public string FindDriverByEmail(string email)
        {
            logger.LogInformation("Finding user with email: {Email}", email);
            var user = _appDb.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return user.Email;
            }
            else
            {
                return "";
            }
        }
        
        public Driver findDriverById(int id)
        {
            logger.LogInformation("Finding driver by ID: {Id}", id);
            db = new DataContext();
            var driver = db.Driver
                .SingleOrDefault(driver => driver.Id == id);
            
   
            return new Driver(driver);
 
        }
        
        public List<Driver> getActiveDrivers()
        {
            logger.LogInformation("Getting all active drivers...");
            db = new DataContext();
            return db.Driver.Where(driver => driver.IsActive).ToList();
        }

        public List<Driver> getAllDrivers()
        {
            logger.LogInformation("Getting all drivers...");
            db = new DataContext();
            drivers = db.Driver
                .Select(d => new Driver(d.Id, d.FirstName, d.LastName)).ToList();

            return drivers;
        }
        
        public int GetAmountOfDrivers() 
        {
            logger.LogInformation("Getting total amount of drivers...");
            db = new DataContext();
            return db.Driver.Count();
        }
    }
}