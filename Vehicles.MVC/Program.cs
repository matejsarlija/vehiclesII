using System.Web;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vehicles.MVC.Profiles;
using Vehicles.Service.Data;
using Vehicles.Service.Models;
using Vehicles.Service.Service.VehicleMakeRepository;
using Vehicles.Service.Service.VehicleModelRepository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VehicleContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("VehicleContext") ?? throw new InvalidOperationException("Connection string 'VehicleContext' not found."), x =>
        x.MigrationsAssembly("Vehicles.Service")));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(depBuilder =>
{
    depBuilder.RegisterType<VehicleMakeRepository>().As<IVehicleMakeRepository>();
    depBuilder.RegisterType<VehicleModelRepository>().As<IVehicleModelRepository>();
});


builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(VehicleMakeProfile));

// Replaced with Autofac 
//builder.Services.AddTransient<IVehicleMakeRepository, VehicleMakeRepository>();
//builder.Services.AddTransient<IVehicleModelRepository, VehicleModelRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<VehicleContext>();
    SeedData.Initialize(services);
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
