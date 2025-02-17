using System.Globalization;
using ApexCharts;
using BrightInvest.Application.Mappings;
using BrightInvest.Infrastructure.DataBase;
using BrightInvest.Infrastructure.DependencyInjections;
using BrightInvest.Web.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container, including DbContext
builder.Services.AddDbContext<DataContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
	options.RootDirectory = "/Web";
});

builder.Services.AddMudServices();

builder.Services.AddScoped<CustomHttpClientService>();

builder.Services.AddApplicationServices();

builder.Services.AddControllers(options =>
{
    // Apply global prefix convention for routing
    options.Conventions.Add(new RoutePrefixConvention("api"));
});

builder.Services.AddServerSideBlazor();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddApexCharts();

var app = builder.Build();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
	dbContext.Database.EnsureCreated(); // Creates the database if it does not exist
	//dbContext.Database.Migrate();
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
