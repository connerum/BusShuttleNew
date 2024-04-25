# Database Migration and Update Instructions

## Commands to Run First

### Add Migrations
```bash
dotnet ef migrations add InitialCreate --context DomainModel.DataContext
dotnet ef migrations add InitialAuthCreate --context BusShuttleManager.Data.AppDbContext

### Add Migrations
```bash
dotnet ef database update --context DomainModel.DataContext
dotnet ef database update --context BusShuttleManager.Data.AppDbContext
