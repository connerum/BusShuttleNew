using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BusShuttleManager.Models;
using DomainModel;
using BusShuttleManager.Services;

namespace BusShuttleManager.Services;

public class HomeController : Controller 
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    
    IDriverService driverService;
    IBusService busService;
    IRouteService routeService;
    IStopService stopService;
    IEntryService entryService;
    ILoopService loopService;



    public HomeController(ILogger<HomeController> logger, IDriverService driverService, IBusService busService, 
        IRouteService routeService, IStopService stopService, IEntryService entryService, ILoopService loopService, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
        this.driverService = driverService;
        this.busService = busService;
        this.routeService = routeService;
        this.stopService = stopService;
        this.entryService = entryService;
        this.loopService = loopService;
    }

    [Authorize(Policy = "DriverAccess")]
    public IActionResult DriverPage()
    {
        var loops = loopService.getActiveLoops(); 
        var busses = busService.getActiveBusses();
        var drivers = driverService.getAllDrivers();

        var viewModel = Models.ViewDriverPage.FromData(loops, busses, drivers);
        return View(viewModel);
    }
    
    [Authorize(Policy = "ManagerAccess")]
    public IActionResult Drivers()
    {
        var userClaims = User.Claims.ToList();
        
        foreach (var claim in userClaims)
        {
            _logger.LogInformation($"Access Type: {claim.Type}, Value: {claim.Value}");
        }
        
        var hasManagerClaim = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Manager");
        _logger.LogInformation($"User has Manager Access: {hasManagerClaim}");

        return View(driverService.getActiveDrivers().Select(d => ViewDriver.FromDriver(d)));
    }
    
    [Authorize(Policy = "ManagerAccess")]
    public IActionResult ManagerPage()
    {
        return View();
    }
    
    [Authorize(Policy = "ManagerAccess")]
    public IActionResult UpdateDriver([FromRoute] int id)
    {
        var driverEditModel = new EditDriver();  
        var driver = driverService.findDriverById(id);
 
        driverEditModel = Models.EditDriver.FromDriver(driver);
        return View(driverEditModel);
    }

    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateDriver(int id, [Bind("FirstName,LastName")] EditDriver driver)
    {
        driverService.UpdateDriverById(id, driver.FirstName, driver.LastName);
        return RedirectToAction("Drivers");
    }

    [Authorize(Policy = "ManagerAccess")]
    public IActionResult CreateDriver()
    {
        var driverCreateModel = Models.CreateDriver.NewDriver(driverService.GetAmountOfDrivers());
        return View(driverCreateModel);
    }
    
    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDriver(int id, [Bind("FirstName,LastName,Email")] CreateDriver driver)
    {
        _logger.LogInformation("Driver's Email: " + driver.Email);
        _logger.LogInformation("Email Result: " + driverService.FindDriverByEmail(driver.Email));
        if(driverService.FindDriverByEmail(driver.Email) == "")
        {
           
            ModelState.AddModelError("Email", "Email not found");

            var driverCreateModel = Models.CreateDriver.NewDriver(id); 
            return View(driverCreateModel);
        }

        driverService.CreateNewDriver(id, driver.FirstName, driver.LastName, driver.Email);
        var user = await _userManager.FindByEmailAsync(driver.Email);
    
        if (user != null)
        {
            await _userManager.AddClaimAsync(user, new Claim("IsActivated", "true"));
        }
        return RedirectToAction("Drivers");
    }
    
    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteDriver(int id)
    {
        var driver = driverService.findDriverById(id);
        if (driver != null)
        {
            driver.IsActive = false; 
            driverService.deactivateDriver(id);
        }
        return RedirectToAction("Drivers");
    }
    
    [Authorize(Policy = "ManagerAccess")]
    public IActionResult Busses()
    {
        var activeBusses = busService.getActiveBusses(); 
        var busViewModels = activeBusses.Select(bus => new ViewBus
        {
            Id = bus.Id,
            BusName = bus.  BusName
        }).ToList();
        
        return View(busViewModels);
    }


    [Authorize(Policy = "ManagerAccess")]
    public IActionResult UpdateBus([FromRoute] int id)
    {
        var busEditModel =  new EditBus();
        var bus = busService.findBusById(id);

        busEditModel = EditBus.FromBus(bus);
        return View(busEditModel);
    }


    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateBus(int id, [Bind("BusName")] EditBus bus)
    {
        busService.UpdateBusById(id, bus.BusName);
        return RedirectToAction("Busses");
    }
    

    [Authorize(Policy = "ManagerAccess")]
    public IActionResult CreateBus()
    {
        var busCreateModel = Models.CreateBus.NewBus(busService.GetAmountOfBusses());
        return View(busCreateModel);
    }


    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateBus(int id, [Bind("BusName")] CreateBus bus)
    {
        busService.CreateNewBus(id, bus.BusName);
        return RedirectToAction("Busses");
    }


    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteBus(int id)
    {
        var bus = busService.findBusById(id);
        if (bus != null)
        {
            bus.IsActive = false; 
            busService.deactivateBus(id);
        }
        return RedirectToAction("Busses");
    }
    
    [Authorize(Policy = "ManagerAccess")]
    public IActionResult Routes()
    {
        var viewModel = new ViewRoutes
        {
            Loops = loopService.getAllLoops(),
            Routes = new List<Routes>()
        };
        return View(viewModel);
    }

    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    public IActionResult Routes(int selectedLoopId)
    {
        var loops = loopService.getActiveLoops(); 
        var stops = stopService.getActiveStops();
        var selectedLoop = loopService.getLoopById(selectedLoopId); 
        var routes = routeService.getRoutesByLoopId(selectedLoopId); 
        var viewModel = Models.ViewRoutes.FromLoopID(routes, loops, selectedLoop, stops);
        return View(viewModel);
    }

    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    public IActionResult AddRoute(int selectedLoopId, int selectStopId)
    {
        var loops = loopService.getActiveLoops(); 
        var stops = stopService.getActiveStops();
        var selectedLoop = loopService.getLoopById(selectedLoopId); 
        var routes = routeService.getRoutesByLoopId(selectedLoopId); 
        var newOrder = routes.Count + 1;
        routeService.CreateNewRoute(newOrder, selectedLoopId, selectStopId);
        routes = routeService.getRoutesByLoopId(selectedLoopId); 
        var viewModel = Models.ViewRoutes.FromLoopID(routes, loops, selectedLoop, stops);
        return View(viewModel);
    }
    
    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    public IActionResult MoveRouteUp(int selectedLoopId, int routeId)
    {
        var loops = loopService.getActiveLoops(); 
        var stops = stopService.getActiveStops();
        var selectedLoop = loopService.getLoopById(selectedLoopId); 
        var routes = routeService.getRoutesByLoopId(selectedLoopId); 
        routeService.IncreaseRouteOrder(routeId);
        var viewModel = Models.ViewRoutes.FromLoopID(routes, loops, selectedLoop, stops);
        return View(viewModel);
    }
    
    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    public IActionResult MoveRouteDown(int selectedLoopId, int routeId)
    {
        var loops = loopService.getActiveLoops(); 
        var stops = stopService.getActiveStops();
        var selectedLoop = loopService.getLoopById(selectedLoopId); 
        var routes = routeService.getRoutesByLoopId(selectedLoopId); 
        routeService.DecreaseRouteOrder(routeId);
        _logger.LogInformation("Route ID: " + routeId);
        var viewModel = Models.ViewRoutes.FromLoopID(routes, loops, selectedLoop, stops);
        return View(viewModel);
    }
    
    [Authorize(Policy = "ManagerAccess")]
    public IActionResult Loops()
    {
        var activeLoops = loopService.getActiveLoops();
        var loopViewModels = activeLoops.Select(loop => new LoopViewModel
        {
            Id = loop.Id,
            Name = loop.Name
        }).ToList();
        
        return View(loopViewModels);
    }
    
    [Authorize(Policy = "ManagerAccess")]
    public IActionResult UpdateLoop([FromRoute] int id)
    {
        var loopEditModel =  new EditLoop();
        var loop = loopService.getLoopById(id);

        loopEditModel = Models.EditLoop.FromLoop(loop);
        return View(loopEditModel);
    }
    
    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateLoop(int id, [Bind("Name")] EditLoop loop)
    {
        loopService.UpdateLoopById(id, loop.Name);
        return RedirectToAction("Loops");
    }
    
    [Authorize(Policy = "ManagerAccess")]
    public IActionResult CreateLoop()
    {
        var loopCreateModel = Models.CreateLoop.NewLoop(loopService.GetAmountOfLoops());
        return View(loopCreateModel);
    }
    
    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateLoop(int id, [Bind("Name")] CreateLoop loop)
    {
        loopService.CreateNewLoop(id, loop.Name);
        return RedirectToAction("Loops");
    }

    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteLoop(int id)
    {
        var loop = loopService.getLoopById(id);
        if (loop != null)
        {
            loop.IsActive = false; 
            loopService.deactivateLoop(loop);
        }
        return RedirectToAction("Loops");
    }
    
    [Authorize(Policy = "ManagerAccess")]
    public IActionResult Stops()
    {
        var activeStops = stopService.getActiveStops(); 
        var stopView = activeStops.Select(stop => new ViewStop
        {
            Id = stop.Id,
            Name = stop.Name

        }).ToList();
        
        return View(stopView);
    }
    
    [Authorize(Policy = "ManagerAccess")]
    public IActionResult UpdateStop([FromRoute] int id)
    {
        var stopEditModel =  new EditStop();
        var stop = stopService.findStopById(id);

        stopEditModel = Models.EditStop.FromStop(stop);
        return View(stopEditModel);
    }
    
    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateStop(int id, [Bind("Name")] EditStop stop)
    {
        stopService.UpdateStopById(id, stop.Name);
        return RedirectToAction("Stops");
    }

    [Authorize(Policy = "ManagerAccess")]
    public IActionResult CreateStop()
    {
        var stopCreateModel = Models.CreateStop.NewStop(stopService.GetAmountOfStops());
        return View(stopCreateModel);
    }
    
    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateStop(int id, [Bind("Name,Latitude,Longitude")] CreateStop stop)
    {
        stopService.CreateNewStop(id, stop.Name, stop.Latitude, stop.Longitude);
        return RedirectToAction("Stops");
    }

    [Authorize(Policy = "ManagerAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteStop(int id)
    {
        var stop = stopService.findStopById(id);
        if (stop != null)
        {
            stop.IsActive = false; 
            stopService.deactivateStop(id);
        }
        return RedirectToAction("Stops");
    }
    
    [Authorize(Policy = "ManagerAccess")]
    [HttpGet]
    public IActionResult Entries()
    {
        ViewBag.Loops = loopService.getAllLoops();
        var entries = entryService.getAllEntries().ToList();
        var viewModels = entries.Select(entry =>
        {
            var loop = loopService.getLoopById(entry.LoopId);
            var driver = driverService.findDriverById(entry.DriverId);
            var stop = stopService.findStopById(entry.StopId);
            var bus = busService.findBusById(entry.BusId);

            return Models.ViewEntry.FromEntry(entry, loop, driver, stop, bus);
        }).ToList();

        return View(viewModels);
    }

    [Authorize(Policy = "ManagerAccess")]
    [HttpGet]
    public IActionResult SearchEntries([FromQuery]DateTime dateTime, int? loopId = null)
    {
        ViewBag.Loops = loopService.getAllLoops();
        List<ViewEntry> viewModels;

        if (loopId.HasValue)
        {
            var entries = entryService.getEntriesByLoopId(loopId.Value).ToList();
            viewModels = entries.Select(entry =>
            {
                var loop = loopService.getLoopById(entry.LoopId);
                var driver = driverService.findDriverById(entry.DriverId);
                var stop = stopService.findStopById(entry.StopId);
                var bus = busService.findBusById(entry.BusId);

                return Models.ViewEntry.FromEntry(entry, loop, driver, stop, bus);
            }).ToList();
        }
        else
        {
            var entries = entryService.getEntriesByDate(dateTime).ToList();
            _logger.LogInformation("Time:" + dateTime);
            viewModels = entries.Select(entry =>
            {
                var loop = loopService.getLoopById(entry.LoopId);
                var driver = driverService.findDriverById(entry.DriverId);
                var stop = stopService.findStopById(entry.StopId);
                var bus = busService.findBusById(entry.BusId);

                return ViewEntry.FromEntry(entry, loop, driver, stop, bus);
            }).ToList();
        }

        return View("Entries", viewModels);
    }

    [Authorize(Policy = "")]
    public IActionResult DriverCreateEntry(int loopId, int busId, int driverId)
    {
        var routes = routeService.getRoutesByLoopId(loopId);
        var stops = stopService.getAllStops();
        var viewModel = Models.CreateEntry.FromData(stops, routes, loopId, busId, driverId);
        
        var existingEntry = entryService.getEntryForLoopBusDriver(loopId, busId, driverId);
        if (existingEntry != null)
        {
            var stopIndex = viewModel.Routes.FindIndex(r => r.StopId == existingEntry.StopId);
            if (stopIndex != -1 && stopIndex + 1 < viewModel.Routes.Count)
            {
                viewModel.SelectedStopId = viewModel.Routes[stopIndex + 1].StopId;
            }
        }

        _logger.LogInformation("Stop ID: " + viewModel.SelectedStopId);

        return View(viewModel);
    }
    
    [Authorize(Policy = "DriverAccess")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateEntry([Bind("Boarded,LeftBehind,BusId,DriverId,LoopId,SelectedStopId")] CreateEntry entryModel)
    {
        entryService.createNewEntry(
            DateTime.Now,
            entryModel.Boarded,
            entryModel.LeftBehind,
            entryModel.BusId,
            entryModel.DriverId,
            entryModel.LoopId,
            entryModel.SelectedStopId
        );

        var routes = routeService.getRoutesByLoopId(entryModel.LoopId);
        _logger.LogInformation("Stop Id: " + entryModel.SelectedStopId);

        var nextStopIndex = routes.FindIndex(r => r.StopId == entryModel.SelectedStopId) + 1;
        if (nextStopIndex < routes.Count)
        {
            entryModel.SelectedStopId = routes[nextStopIndex].StopId;
        }

        return RedirectToAction("DriverCreateEntry", new
        {
            loopId = entryModel.LoopId,
            busId = entryModel.BusId,
            driverId = entryModel.DriverId,
            selectedStopId = entryModel.SelectedStopId
        });
    }

}