using DomainModel;
namespace BusShuttleManager.Services
{
    public class StopService : IStopService
    {
        private readonly ILogger<StopService> logger;
        private DataContext db;
        List<Stop> stops;

        public StopService(ILogger<StopService> logger)
        {
            this.logger = logger;
        }
        
        public void CreateNewStop(int id, string name, double lat, double lon)
        {
            logger.LogInformation("Creating new stop with ID: {Id}", id);
            db = new DataContext();
            db.Add(new Stop{Id = id, Name=name, Latitude=lat, Longitude=lon});
            db.SaveChanges();
        }
        
        public void deactivateStop(int id)
        {
            logger.LogInformation("Deactivating stop with ID: {Id}", id);
            db = new DataContext();
            var existingStop = db.Stop.FirstOrDefault(s => s.Id == id);
        
            if (existingStop != null)
            {
                existingStop.IsActive = false;
                db.SaveChanges();
            }
        }
        
        public void UpdateStopById(int id, string name)
        {
            logger.LogInformation("Updating stop with ID: {Id}", id);
            db = new DataContext();
            var existingStop = db.Stop.SingleOrDefault(stop => stop.Id == id);
            existingStop.Update(name);

            var stop = db.Stop
                .SingleOrDefault(stop => stop.Id == existingStop.Id);
            
            if(stop != null)
            {
                stop.Name = name;
                db.SaveChanges();
            }
        }
        
        public Stop findStopById(int id)
        {
            logger.LogInformation("Finding stop by ID: {Id}", id);
            db = new DataContext();
            var stop = db.Stop
                .SingleOrDefault(s =>s.Id == id);

            return new Stop(stop);
        }
        
        public List<Stop> getActiveStops()
        {
            logger.LogInformation("Getting all active stops...");
            db = new DataContext();
            return db.Stop.Where(stop => stop.IsActive).ToList();
        }

        public List<Stop> getAllStops()
        {
            logger.LogInformation("Getting all stops...");
            db = new DataContext();
            stops = db.Stop
                .Select(s => new Stop(s.Id, s.Name, s.Latitude, s.Longitude)).ToList();
            return stops;
        }

        public int GetAmountOfStops()
        {
            logger.LogInformation("Getting total amount of stops...");
            db = new DataContext();
            return db.Stop.Count();
        }
    }
}