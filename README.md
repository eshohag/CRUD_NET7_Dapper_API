# .NET 7.0 + Dapper + MS SQL Server - CRUD API


### Dapper ORM (Object Relational Mapper)
The API uses Dapper to access and manage data in the database. Dapper is a micro ORM that supports executing raw SQL queries and mapping results to C# objects and collections, it's implemented as a collection of extension methods on top of the ADO.NET IDbConnection interface.

Dapper doesn't support all of the functionality of full ORMs such as Entity Framework Core (e.g. SQL generation, caching, database migrations etc) but instead is focused on performance and is known for being lightweight and fast.

### NuGet Packages Required
```
    Install-Package AutoMapper -Version 12.0.1
    Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection -Version 12.0.0
    Install-Package BCrypt.Net-Next -Version 4.0.3
    Install-Package Dapper -Version 2.0.123
    Install-Package Microsoft.Data.SqlClient -Version 5.1.0
    Install-Package Swashbuckle.AspNetCore -Version 6.5.0  
```
