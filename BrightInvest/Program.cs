using System.Globalization;
using BrightInvest.Application.Services;
using BrightInvest.Application.UseCases.Assets;
using BrightInvest.Domain.Interfaces;
using BrightInvest.Infrastructure.DataBase;
using BrightInvest.Infrastructure.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container, including DbContext
builder.Services.AddDbContext<DataContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
	options.RootDirectory = "/Web";
});


builder.Services.AddScoped(sp =>
{
	var nav = sp.GetRequiredService<NavigationManager>(); 
	return new HttpClient { BaseAddress = new Uri(nav.BaseUri) }; 
});

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAssetUseCase, AssetUseCase>();

builder.Services.AddControllers(options =>
{
    // Apply global prefix convention for routing
    options.Conventions.Add(new RoutePrefixConvention("api"));
});

builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
	dbContext.Database.EnsureCreated(); // Creates the database if it does not exist
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapControllers();

app.MapFallbackToPage("/_Host", "/Web/_Host");

app.Run();
