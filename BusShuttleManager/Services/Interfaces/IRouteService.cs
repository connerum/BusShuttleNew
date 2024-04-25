using DomainModel;

namespace BusShuttleManager.Services
{
    public interface IRouteService
    {
        public void CreateNewRoute(int order, int loopId, int stopId);
        public void UpdateRouteById(int id, int order);
        public Routes findRouteById(int id);
        public List<Routes> getRoutesByLoopId(int loopId);
        public List<Routes> getAllRoutes();
        public int GetAmountOfRoutes();
        public void IncreaseRouteOrder(int id);
        public void DecreaseRouteOrder(int id);
    }
}