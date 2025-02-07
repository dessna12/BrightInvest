﻿using BrightInvest.Application.DTOs.AssetPrices;
using BrightInvest.Application.Services.AlphaVantage;
using BrightInvest.Application.Services.Date;
using BrightInvest.Application.UseCases.Interfaces;
using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Interfaces;
using BrightInvest.Infrastructure.Repository;
using BrightInvest.Web.Pages;

namespace BrightInvest.Application.UseCases.AssetPrices
{
	public class AssetPriceUseCase : IAssetPriceUseCase
	{

		private readonly IAssetPriceRepository _assetPriceRepository;
		private readonly IAssetRepository _assetRepository;
		private readonly IAlphaVantageService _alphaVantageService;

		public AssetPriceUseCase(
			IAssetPriceRepository assetPriceRepository,
			IAssetRepository assetRepository,
			IAlphaVantageService alphaVantageService
			)
		{
			_assetPriceRepository = assetPriceRepository;
			_assetRepository = assetRepository;
			_alphaVantageService = alphaVantageService;
		}

		public async Task<IEnumerable<AssetPriceDto>> GetAllAssetPricesAsync()
		{
			var assetPrices = await _assetPriceRepository.GetAllAssetPricesAsync();
			return assetPrices.Select(assetPrice => MapToAssetPriceDto(assetPrice));
		}

		public async Task<AssetPriceDto> GetAssetPriceByIdAsync(Guid id)
		{
			var assetPrice = await _assetPriceRepository.GetAssetPriceByIdAsync(id);
			if (assetPrice == null)
				throw new KeyNotFoundException($"Asset with ID {id} not found.");
			return MapToAssetPriceDto(assetPrice);
		}

		public async Task<IEnumerable<AssetPriceDto>> GetAllAssetPricesByAssetIdAsync(Guid assetId)
		{
			var assetPrices = await _assetPriceRepository.GetAllAssetPricesByAssetIdAsync(assetId);
			return assetPrices
					.OrderBy(assetPrice => assetPrice.Date)
					.Select(assetPrice => MapToAssetPriceDto(assetPrice));
		}

		public async Task<IEnumerable<AssetPriceDto>> GetAllAssetPricesBySymbolAsync(string symbol)
		{
			// Case 1: The asset is not in the Asset List
			var asset = await _assetRepository.GetAssetBySymbolAsync(symbol);
			if (asset == null)
			{
				throw new KeyNotFoundException($"Asset with symbol {symbol} not found.");
			}

			// Case 2: Check if we have recent prices in the database
			var lastPrice = await _assetPriceRepository.GetLatestPriceByAssetIdAsync(asset.Id);
			DateTime lastWorkingDay = DateService.GetLastWorkingDay(DateTime.UtcNow.Date);

			// If not Fetch
			DateTime? fromDate = lastPrice?.Date.AddDays(1);			
			if (lastPrice == null || lastPrice.Date != lastWorkingDay)
			{
				var stockDataList = await _alphaVantageService.FetchAllStockPricesAsync(symbol, fromDate);
				if (stockDataList == null || !stockDataList.Any())
				{
					throw new Exception($"No stock data found for symbol: {symbol}");
				}

				// Add in DB
				var assetPricesToAdd = stockDataList.Select(s => new AssetPrice(asset.Id, s.Date, s.ClosePrice)).ToList();
				await _assetPriceRepository.AddAssetPricesAsync(assetPricesToAdd);

			}

			var assetPricesDTO = await GetAllAssetPricesByAssetIdAsync(asset.Id);
			//var assetPrices = await _assetPriceRepository.GetAllAssetPricesByAssetIdAsync(asset.Id);
			//var assetPriceDTO = assetPrices.Select(assetPrice => MapToAssetPriceDto(assetPrice));
			return assetPricesDTO;
		}

		public async Task<AssetPriceDto> CreateAssetPriceAsync(AssetPriceCreateDto assetCreateDto)
		{
			var assetPrice = new AssetPrice(assetCreateDto.AssetId, assetCreateDto.Date, assetCreateDto.ClosePrice);
			await _assetPriceRepository.AddAssetPriceAsync(assetPrice);

			return new AssetPriceDto(assetPrice.Id, assetPrice.AssetId, assetPrice.Date, assetPrice.ClosePrice);
		}
		public async Task<bool> DeleteAssetPriceAsync(Guid id)
		{
			return await _assetPriceRepository.DeleteAssetPriceAsync(id);
		}

		public async Task<bool> DeleteAllAssetPricesByAssetIdAsync(Guid assetId)
		{
			return await _assetPriceRepository.DeleteAllAssetPricesByAssetIdAsync(assetId);
		}

		private AssetPriceDto MapToAssetPriceDto(AssetPrice assetPrice)
		{
			return new AssetPriceDto(assetPrice.Id, assetPrice.AssetId, assetPrice.Date, assetPrice.ClosePrice);
		}



	}
}
