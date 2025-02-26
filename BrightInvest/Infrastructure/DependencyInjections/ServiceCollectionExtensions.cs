using BrightInvest.Application.Services.AlphaVantage;
using BrightInvest.Application.Services.AssetMetrics;
using BrightInvest.Application.Services.Excel;
using BrightInvest.Application.UseCases.AssetPrices;
using BrightInvest.Application.UseCases.Assets;
using BrightInvest.Application.UseCases.Currencies;
using BrightInvest.Application.UseCases.FinancialIndicators;
using BrightInvest.Application.UseCases.Interfaces;
using BrightInvest.Domain.Interfaces;
using BrightInvest.Infrastructure.Repositories;
using BrightInvest.Infrastructure.Repository;
using FluentValidation;

namespace BrightInvest.Infrastructure.DependencyInjections
{
	public static class ServiceCollectionExtensions
	{

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IAssetRepository, AssetRepository>();
			services.AddScoped<IAssetUseCase, AssetUseCase>();

			services.AddScoped<IAssetMetricsUseCase, AssetMetricsUseCase>();
			services.AddScoped<IAssetMetricsService, AssetMetricsService>();
			services.AddScoped<IAssetIndicatorService, AssetIndicatorService>();

			services.AddScoped<IAssetPriceRepository, AssetPriceRepository>();
			services.AddScoped<IAssetPriceUseCase, AssetPriceUseCase>();

			services.AddScoped<IAlphaVantageService, AlphaVantageService>();

			services.AddScoped<ICurrencyRepository, CurrencyRepository>();
			services.AddScoped<ICurrencyUseCase, CurrencyUseCase>();

			services.AddScoped<IValidator<AssetCreateDto>, AssetCreateDtoValidator>();
			services.AddScoped<IValidator<AssetUpdateDto>, AssetUpdateDtoValidator>();

			services.AddScoped<ExcelService>();

			return services;
		}

	}
}
