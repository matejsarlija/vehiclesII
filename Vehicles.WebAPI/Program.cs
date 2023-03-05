using System.Configuration;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Vehicles.Model.Common.Profiles;
using Vehicles.Repository;
using Vehicles.Repository.Common;
using Vehicles.Service.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VehicleContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("VehicleContext") ?? throw new InvalidOperationException("Connection string 'VehicleContext' not found."), x =>
        x.MigrationsAssembly("Vehicles.Service")));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(depBuilder =>
{
    depBuilder.RegisterType<VehicleUnitOfWork>().As<IVehicleUnitOfWork>();
});


builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(VehiclesApiProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<VehicleContext>();
    SeedData.Initialize(services);

    context.Database.EnsureCreated();
    
    // if (context.Database.GetDbConnection() is SqliteConnection conn)
    // {
    //     SqliteConnection.ClearPool(conn);
    // }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
