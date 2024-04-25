using DomainModel;
using Microsoft.Extensions.Logging;
namespace BusShuttleManager.Services
{
    public class BusService : IBusService
    {
        private readonly ILogger<BusService> logger;
        private DataContext db;
        List<Bus> busses;


        public BusService(ILogger<BusService> logger)
        { 
            this.logger = logger;
        }
        
        public void CreateNewBus(int id, string name)
        {
            logger.LogInformation("Creating new bus with ID: {Id}", id);
            db = new DataContext();
            db.Add(new Bus{Id=id, BusName=name});
            db.SaveChanges();
        }
        
        public void deactivateBus(int id)
        {
            logger.LogInformation("Deactivating bus with ID: {Id}", id);
            db = new DataContext();
            var existingBus = db.Bus.FirstOrDefault(b => b.Id == id);
        
            if (existingBus != null)
            {
                existingBus.IsActive = false;
                db.SaveChanges();
            }
        }
        
        public void UpdateBusById(int id, string name)
        {
            logger.LogInformation("Updating bus with ID: {Id}", id);
            db = new DataContext();
            var existingBus = db.Bus.SingleOrDefault(bus => bus.Id == id);
            existingBus.Update(name);

            var bus = db.Bus
                .SingleOrDefault(bus => bus.Id == existingBus.Id);
            
            if(bus != null)
            {
                bus.BusName = name;
                db.SaveChanges();
            }
        }
        
        public Bus findBusById(int id)
        {
            logger.LogInformation("Finding bus by ID: {Id}", id);
            db = new DataContext();
            var bus = db.Bus
                .SingleOrDefault(bus =>bus.Id == id);

            return new Bus(bus);
        }
        
        public List<Bus> getActiveBusses()
        {
            logger.LogInformation("Getting active busses...");
            db = new DataContext();
            return db.Bus.Where(bus => bus.IsActive).ToList();
        }

        public List<Bus> getAllBusses()
        {
            logger.LogInformation("Getting all busses...");
            db = new DataContext();
            busses = db.Bus
                .Select(b => new Bus(b.Id, b.BusName)).ToList();
            return busses;
        }

        public int GetAmountOfBusses()
        {
            logger.LogInformation("Getting total amount of busses...");
            db = new DataContext();
            return db.Bus.Count();
        }

        
    }
}