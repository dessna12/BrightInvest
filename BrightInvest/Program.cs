using System.Globalization;
using ApexCharts;
using BrightInvest.Application.Mappings;
using BrightInvest.Application.Services.AlphaVantage;
using BrightInvest.Application.Services.Date;
using BrightInvest.Application.Services.Excel;
using BrightInvest.Application.UseCases.AssetPrices;
using BrightInvest.Application.UseCases.Assets;
using BrightInvest.Application.UseCases.Currencies;
using BrightInvest.Application.UseCases.Interfaces;
using BrightInvest.Domain.Interfaces;
using BrightInvest.Infrastructure.DataBase;
using BrightInvest.Infrastructure.Repositories;
using BrightInvest.Infrastructure.Repository;
using BrightInvest.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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

//builder.Services.AddScoped<BrightInvest.Application.Services.Asset.IAssetUseCase, AssetService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAssetUseCase, AssetUseCase>();

//builder.Services.AddScoped<IAssetPriceService, AssetPriceService>();
builder.Services.AddScoped<IAssetPriceRepository, AssetPriceRepository>();
builder.Services.AddScoped<IAssetPriceUseCase, AssetPriceUseCase>();

builder.Services.AddScoped<IAlphaVantageService, AlphaVantageService>();

builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<ICurrencyUseCase, CurrencyUseCase>();

builder.Services.AddScoped<ExcelService>();

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
