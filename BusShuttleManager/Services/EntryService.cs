using DomainModel;
using Microsoft.Extensions.Logging;
namespace BusShuttleManager.Services
{
    public class EntryService : IEntryService
    {
        private readonly ILogger<EntryService> logger;
        private DataContext db;
        List<Entry> entries;

        public EntryService(ILogger<EntryService> logger)
        {
            this.logger = logger;
        }
        
        public void createNewEntry(DateTime timeStamp, int boarded, int leftBehind, int busId, int driverId, int loopId, int stopId)
        {
            logger.LogInformation("Creating new entry...");
            db = new DataContext();
            var totalEntries = db.Entry.Count();
            db.Add(new Entry{Id = totalEntries+1, TimeStamp=timeStamp, Boarded=boarded, LeftBehind=leftBehind, 
                BusId=busId, DriverId=driverId, LoopId=loopId, StopId=stopId});
            db.SaveChanges();
        }
        
        public Entry findEntryById(int id)
        {
            logger.LogInformation("Finding entry by ID: {Id}", id);
            db = new DataContext();
            var entry = db.Entry
                .SingleOrDefault(e =>e.Id == id);

            return new Entry(entry);
        }
        
        public List<Entry> getEntriesByDate(DateTime selectedDate)
        {
            logger.LogInformation("Getting entries by date: {SelectedDate}", selectedDate);
            db = new DataContext();
            return db.Entry
                .Where(entry => entry.TimeStamp.Date == selectedDate.Date)
                .ToList();
        }

        public List<Entry> getEntriesByLoopId(int loopId)
        {
            logger.LogInformation("Getting entries by loop Id: {Id}", loopId);
            db = new DataContext();
            return db.Entry
                .Where(entry => entry.LoopId == loopId)
                .ToList();
        }
        
        public List<Entry> getEntriesByDateAndLoop(DateTime dateTime, int loopId)
        {
            logger.LogInformation("Getting entries by loop Id: {Id} and Date: {SelectedDate}", loopId, dateTime);
            db = new DataContext();
            return db.Entry
                .Where(entry => entry.TimeStamp.Date == dateTime.Date && entry.LoopId == loopId)
                .ToList();
        }

        public Entry getEntryForLoopBusDriver(int loopId, int busId, int driverId)
        {
            logger.LogInformation("Getting entry with loop Id: {LoopId}, bus Id: {BusId} and driver Id: {DriverId}", loopId, busId, driverId);
            db = new DataContext();
            return db.Entry.FirstOrDefault(e => e.LoopId == loopId && e.BusId == busId && e.DriverId == driverId);
        }

        public List<Entry> getAllEntries()
        {
            logger.LogInformation("Getting all entries...");
            db = new DataContext();
            entries = db.Entry
                .Select(e => new Entry(e.TimeStamp, e.Boarded, e.LeftBehind, e.LoopId, e.DriverId, e.StopId, e.BusId)).ToList();
            return entries;
        }
        
    }
}