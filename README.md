# Database.EntityFramework.SqlServer

## Migrations

Create a new migration and update the database:
Leave the API project set as the main project, go to "Package Manager Console" and enter the code below:

```
Add-Migration -OutputDir Migrations [NameMigration] -Project API.Database.SqlServer.Db -StartupProject API.Database.SqlServer.Service -Context ApplicationDbContext

Update-Database -Project API.Database.SqlServer.Db -StartupProject API.Database.SqlServer.Service
```

## Settings

Example of a `secrets` configuration file:
Right-click on the API project or another project within the solution and click on "Manager User Secrets"

```json
{
  "SqlConnectionString": "Server=localhost;Database=test_db_;User Id=sa;Password=Pass123$;TrustServerCertificate=True"
}
```