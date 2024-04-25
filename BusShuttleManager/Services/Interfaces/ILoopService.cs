using DomainModel;

namespace BusShuttleManager.Services
{
    public interface ILoopService
    {
        public void CreateNewLoop(int id, string name);
        public void deactivateLoop(Loop loop);
        public void UpdateLoopById(int id, string name);
        public Loop getLoopById(int id);
        public List<Loop> getActiveLoops();
        public List<Loop> getAllLoops();
        public int GetAmountOfLoops();
    }
}