using DomainModel;

namespace BusShuttleManager.Services
{
    public interface IBusService
    {
        public void CreateNewBus(int id, string name);
        public void deactivateBus(int id);
        public void UpdateBusById(int id, string name);
        public Bus findBusById(int id);
        public List<Bus> getActiveBusses();
        public List<Bus> getAllBusses();
        public int GetAmountOfBusses();
    }
}