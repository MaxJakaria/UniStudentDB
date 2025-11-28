# Project Setup Guide

## Required NuGet Packages

Install the following NuGet packages to enable database, validation, error handling, and documentation features:

```
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package ErrorOr
Install-Package Swashbuckle.AspNetCore
Install-Package FluentValidation
```

## Database Setup

Generate the initial database schema and apply migrations using Entity Framework Core:

```
Add-Migration InitialCreate
Update-Database
```

## Features Included

### **Entity Framework Core**

* SQL Server provider
* Migration and database update tooling

### **Error Handling**

* Using ErrorOr package for functional-style error handling

### **Swagger (API Documentation)**

* Swashbuckle for auto-generated API docs and testing UI

### **FluentValidation**

* Input validation with clean and maintainable validation rules

## Running the Project

1. Restore NuGet packages
2. Apply migrations if needed
3. Run the application
4. Access Swagger UI at `/swagger`

## Notes

* Ensure SQL Server is running before applying migrations.
* Update your connection string in `appsettings.json` as needed.
