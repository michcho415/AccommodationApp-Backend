# AccomodationApp

Web application for managing apartment rentals.

## First running

### Creating migration:
```powershell
dotnet ef migrations add InitialCreate --project Models --startup-project AccommodationApp --context DatabaseContext
```
### Creating database:
```powershell
dotnet ef database update --project Models --startup-project AccommodationApp --context DatabaseContext
```

## Remove a migration
```powershell
dotnet ef migrations remove --project Models --startup-project AccommodationApp
```

## Prerequisities:
- .NET 6.0
- Installed SQL Server/SQL Express

## Setting

### Deployment
- configure Cors

### ConnectionString appsettings.json
```xml
"ConnectionStrings": {
    "DefaultConnection": "Server=<SERVER_NAME>;Database=<DATABASE_NAME>;Integrated Security=true;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
