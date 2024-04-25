using DomainModel;
namespace BusShuttleManager.Services
{
    public class RouteService : IRouteService
    {
        private readonly ILogger<RouteService> logger;
        private DataContext db;
        List<Routes> routess;

        public RouteService(ILogger<RouteService> logger)
        {
            db = new DataContext();
            this.logger = logger;
        }
        
        public void CreateNewRoute(int order, int loopId, int stopId)
        {
            logger.LogInformation("Creating new route... ");
            db = new DataContext();
            var totalRoutess = db.Routes.Count();
            db.Add(new Routes{Id = totalRoutess + 1, LoopId=loopId, Order=order, StopId=stopId});
            db.SaveChanges();
        }
        
        public void UpdateRouteById(int id, int order)
        {
            logger.LogInformation("Updating route by ID: {Id}", id);
            db = new DataContext();
            var existingRoutes = db.Routes.SingleOrDefault(Routes => Routes.Id == id);
            existingRoutes.Update(order);

            var Routes = db.Routes
                .SingleOrDefault(Routes => Routes.Id == existingRoutes.Id);
            
            if(Routes != null)
            {
                Routes.Order = order;
                db.SaveChanges();
            }
        }
        
        public Routes findRouteById(int id)
        {
            logger.LogInformation("Finding route by ID: {Id}", id);
            db = new DataContext();
            var route = db.Routes
                .SingleOrDefault(r =>r.Id == id);

            return new Routes(route);
        }
        
        public List<Routes> getRoutesByLoopId(int loopId)
        {
            logger.LogInformation("Getting routes by loop ID: {LoopId}", loopId);
            db = new DataContext();
            var routes = db.Routes.Where(r => r.LoopId == loopId).ToList();
            return routes;
        }

        public List<Routes> getAllRoutes()
        {
            logger.LogInformation("Getting all routes...");
            db = new DataContext();
            routess = db.Routes
                .Select(r => new Routes(r.Id, r.Order)).ToList();
            return routess;
        }

        public int GetAmountOfRoutes()
        {
            logger.LogInformation("Getting total amount of routes...");
            db = new DataContext();
            return db.Routes.Count();
        }
        
        
        public void IncreaseRouteOrder(int id)
        {
            using (db = new DataContext())
            {
                var route = db.Routes.FirstOrDefault(r => r.Id == id);
                if (route != null && route.Order > 1)
                {
                    var prevRoute = db.Routes.FirstOrDefault(r => r.Order == route.Order - 1);
                    if (prevRoute != null)
                    {
                        prevRoute.Order++;
                        route.Order--;
                        db.SaveChanges();
                    }
                }
            }
        }

        public void DecreaseRouteOrder(int id)
        {
            using (db = new DataContext())
            {
                var route = db.Routes.FirstOrDefault(r => r.Id == id);
                if (route != null)
                {
                    var nextRoute = db.Routes.FirstOrDefault(r => r.Order == route.Order + 1);
                    if (nextRoute != null)
                    {
                        nextRoute.Order--;
                        route.Order++;
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}