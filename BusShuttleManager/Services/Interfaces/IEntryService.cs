using DomainModel;

namespace BusShuttleManager.Services
{
    public interface IEntryService
    { 
        public void createNewEntry(DateTime timeStamp, int boarded, int leftBehind, int busId, int driverId, int loopId, int stopId);
        public Entry findEntryById(int id);
        public List<Entry> getEntriesByDate(DateTime dateTime);
        public List<Entry> getEntriesByLoopId(int loopId);
        public List<Entry> getEntriesByDateAndLoop(DateTime dateTime, int loopId);
        public Entry getEntryForLoopBusDriver(int loopId, int busId, int driverId);
        public List<Entry> getAllEntries();
    }
}