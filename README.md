# CrudAPIExample
dotnet web api core application
#**Scaffold**
Scaffold-DbContext "Server=DESKTOP-SEC5LT3;Database=sampledb;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context ApplicationDbContext -Force

#**ConnectionStrings**
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-SEC5LT3;Database=prod;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "AllowedHosts": "*"
}
