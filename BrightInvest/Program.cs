//using BrightInvest.Data;
using System.Globalization;
using BrightInvest.Infrastructure.DataBase;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


Console.WriteLine($"Loaded Connection String: {builder.Configuration.GetConnectionString("DefaultConnection")}");
// Add services to the container, including DbContext
builder.Services.AddDbContext<DataContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
	options.RootDirectory = "/Web";
});

builder.Services.AddControllers();
//builder.Services.AddRazorPages();
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
