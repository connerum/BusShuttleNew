using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BusShuttleManager.Data;
using BusShuttleManager.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerAccess", policy =>
    {
        policy.RequireRole("Manager");
    });

    options.AddPolicy("DriverAccess", policy =>
    {
        policy.RequireAuthenticatedUser(); 
        policy.RequireRole("Driver");
        policy.RequireClaim("IsActivated", "true"); 
    });
});

builder.Services.AddRazorPages();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<ILoopService, LoopService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IStopService, StopService>();
builder.Services.AddScoped<IEntryService, EntryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.MapRazorPages();
app.Run();
