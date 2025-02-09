using BrightInvest.Application.DTOs.AssetPrices;
using BrightInvest.Application.Exceptions;
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
			var asset = await _assetRepository.GetAssetBySymbolAsync(symbol);

			// Check if we have recent prices in the database
			var lastPrice = await _assetPriceRepository.GetLatestPriceByAssetIdAsync(asset.Id);
			DateTime lastWorkingDay = DateService.GetLastWorkingDay(DateTime.UtcNow.Date);

			// If not Fetch
			DateTime? fromDate = lastPrice?.Date.AddDays(1);			
			if (lastPrice == null || lastPrice.Date != lastWorkingDay)
			{
				var stockDataList = await _alphaVantageService.FetchAllStockPricesAsync(symbol, fromDate);

				// Add in DB
				var assetPricesToAdd = stockDataList.Select(s => new AssetPrice(asset.Id, s.Date, s.ClosePrice)).ToList();
				await _assetPriceRepository.AddAssetPricesAsync(assetPricesToAdd);
			}

			var assetPricesDTO = await GetAllAssetPricesByAssetIdAsync(asset.Id);
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
			try
			{
				return new AssetPriceDto(assetPrice.Id, assetPrice.AssetId, assetPrice.Date, assetPrice.ClosePrice);
			}
			catch (Exception ex)
			{
				throw new UseCaseException($"Failed to map asset price: {ex.Message}", ex);
			}
		}
	}
}
