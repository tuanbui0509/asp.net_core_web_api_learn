# asp.net_core_web_api_learn

# Install packages
1. EF CORE
**https://learn.microsoft.com/en-us/ef/core/get-started/overview/install**
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
- dotnet tool install --global dotnet-ef
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package Microsoft.EntityFrameworkCore.Tools
- dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer 
## Syntax migration
- dotnet ef database update
- dotnet ef database drop
- dotnet ef migrations add
- dotnet ef migrations remove

