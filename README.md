## Vehicles App

This is an asp.net app made for a job application, consisting almost solely
from tips out of official Microsoft tutorial docs for asp.net, one for MVC/Razor part and one for "Data Access" part.

1.**[MVC/Razor](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-7.0&tabs=visual-studio)**

2.**[Data access](https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-7.0)**

In the first part you'll find the needed information (that you will have to fit for your use case, of course) to build out
the search and paging for your app, while data acccess related tutorial can help you with a filter style search.

You will find the code for **PaginatedList** there as well.

To make paging work with a filter on your database / entity model you will "have to" fit it to an adequate view model, depending on your use case, this will provide an example.

### Thoughts
This is a relatively bad take on a Repository pattern because I did it "backwards" in a sense.

I am using .net on Linux (Fedora) with Rider with SQlite as the db so the search on VehicleMakes is case-sensitive since it is db dependent.

Since the split to **Vehicles.MVC** and **Vehicles.Service**, and placing the DbContext in the way I did, we need this extra bit of setup in Program.cs:

    `builder.Services.AddDbContext<VehicleContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("VehicleContext")
        ?? throw new InvalidOperationException("Connection string 'VehicleContext' not found."), x => x.MigrationsAssembly("Vehicles.Service")));`

        
and do this from the Vehicles.MVC folder:

`dotnet ef migrations add InitialMIgration --project Vehicles.Service`

`dotnet ef database update --project Vehicles.Service`

Parts of CRUD may work funny for you under Linux unless you disable **nullable** in the project settings due to a .NET bug,
should be fine on Windows.