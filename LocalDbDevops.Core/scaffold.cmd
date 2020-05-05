dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef dbcontext scaffold "Server=(localdb)\mssqllocaldb;Database=LocalDbDevops" Microsoft.EntityFrameworkCore.SqlServer -d --context DevopsDbContext -o Entities
