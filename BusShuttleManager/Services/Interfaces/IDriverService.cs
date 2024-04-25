using DomainModel;

namespace BusShuttleManager.Services
{
    public interface IDriverService
    {
        public void CreateNewDriver(int id, string fName, string lName, string email);
        public void deactivateDriver(int id);
        public void UpdateDriverById(int id, string fName, string lName);
        public Driver findDriverById(int id);
        public string FindDriverByEmail(string email);
        public List<Driver> getActiveDrivers();
        public List<Driver> getAllDrivers();
        public int GetAmountOfDrivers();
    }
}