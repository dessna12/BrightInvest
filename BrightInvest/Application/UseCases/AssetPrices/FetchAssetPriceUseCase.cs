using BrightInvest.Application.DTOs.AlphaVantage;
using System.Collections.Generic;
using BrightInvest.Application.Services.AlphaVantage;
using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Interfaces;

namespace BrightInvest.Application.UseCases.AssetPrices
{
	public class FetchAssetPriceUseCase
	{
		private readonly IAlphaVantageService _alphaVantageService;
		private readonly IAssetRepository _assetRepository;
		private readonly IAssetPriceRepository _assetPriceRepository;

		public FetchAssetPriceUseCase(
			IAlphaVantageService alphaVantageService,
			IAssetRepository assetRepository,
			IAssetPriceRepository assetPriceRepository)
		{
			_alphaVantageService = alphaVantageService;
			_assetRepository = assetRepository;
			_assetPriceRepository = assetPriceRepository;
		}

		public async Task ExecuteAsync(string symbol)
		{
			var asset = await _assetRepository.GetAssetBySymbolAsync(symbol);
			if (asset == null)
			{
				throw new KeyNotFoundException($"No asset found for symbol {symbol}");
			}

			var stockDataList = await _alphaVantageService.FetchAllStockPricesAsync(symbol);
			if (!stockDataList.Any())
			{
				throw new Exception($"No stock data found for symbol {symbol}");
			}

			var assetPrices = stockDataList.Select(s => new AssetPrice(asset.Id, s.Date, s.ClosePrice)).ToList();
			await _assetPriceRepository.AddAssetPricesAsync(assetPrices);
		}
	}
}
