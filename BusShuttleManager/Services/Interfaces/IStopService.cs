using DomainModel;

namespace BusShuttleManager.Services
{
    public interface IStopService
    {
        public void CreateNewStop(int id, string name, double lat, double lon);
        public void deactivateStop(int id);
        public void UpdateStopById(int id, string name);
        public Stop findStopById(int id);
        public List<Stop> getActiveStops();
        public List<Stop> getAllStops();
        public int GetAmountOfStops();
    }
}