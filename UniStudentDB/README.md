# Required NuGet Packages
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools

# For functional programming and error handling
Install-Package ErrorOr

# For code generation of database context and entities
Add-Migration InitialCreate
Update-Database

# For API Documentation and Testing UI (Swagger)
Install-Package Swashbuckle.AspNetCore