using BrightInvest.Domain.Interfaces;
using BrightInvest.Domain.Entities;
using BrightInvest.Application.UseCases.Interfaces;
using AutoMapper;
using BrightInvest.Application.DTOs.Assets;
using FluentValidation;
using Humanizer;

namespace BrightInvest.Application.UseCases.Assets
{
	public class AssetUseCase : IAssetUseCase
	{
		private readonly IAssetRepository _assetRepository;
		private readonly IMapper _mapper;
		private readonly IValidator<AssetCreateDto> _validatorAssetCreate;
		private readonly IValidator<AssetUpdateDto> _validatorAssetUpdate;
		public AssetUseCase(IAssetRepository assetRepository, IMapper mapper, IValidator<AssetCreateDto> validatorAssetCreate, IValidator<AssetUpdateDto> validatorAssetUpdate)
		{
			_assetRepository = assetRepository;
			_mapper = mapper;
			_validatorAssetCreate = validatorAssetCreate;
			_validatorAssetUpdate = validatorAssetUpdate;
		}

		public async Task<IEnumerable<AssetDto>> GetAllAssetsAsync()
		{
			var assets = await _assetRepository.GetAllAssetsAsync();
			return assets.Select(asset => _mapper.Map<AssetDto>(asset));
		}

		public async Task<AssetDto> GetAssetByIdAsync(Guid assetId)
		{
			Asset asset = await _assetRepository.GetAssetByIdAsync(assetId);
			return _mapper.Map<AssetDto>(asset);
			//return new AssetDto(asset.Id, asset.Ticker, asset.Name, asset.Currency.ToString());
		}

		public async Task<AssetDto> CreateAssetAsync(AssetCreateDto assetCreateDto)
		{

			var validationResult = await _validatorAssetCreate.ValidateAsync(assetCreateDto);
			if (!validationResult.IsValid)
			{
				var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
				throw new ValidationException($"Validation failed: {string.Join("; ", errorMessages)}");
			}

			Asset asset = _mapper.Map<Asset>(assetCreateDto);
			await _assetRepository.AddAssetAsync(asset);

			return _mapper.Map<AssetDto>(asset);
		}

		public async Task<AssetsCreateResponseDto> CreateAssetsAsync(List<AssetCreateDto> assetCreateDtos)
		{

			var successfulAssets = new List<AssetDto>();
			var errorMessages = new List<string>();

			foreach (var dto in assetCreateDtos)
			{
				var validationResult = await _validatorAssetCreate.ValidateAsync(dto);
				if (!validationResult.IsValid)
				{
					var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
					errorMessages.Add($"Error processing asset {dto.Ticker}: {errors}");
					continue;  // Skip to the next asset
				}

				var asset = _mapper.Map<Asset>(dto);
				try
				{
					var assets = _mapper.Map<List<Asset>>(assetCreateDtos);
					await _assetRepository.AddAssetsAsync(assets);
				}
				catch (Exception ex)
				{
					errorMessages.Add($"Failed to add asset {dto.Ticker}: {ex.Message}");
				}
			}

			return new AssetsCreateResponseDto
			{
				Success = successfulAssets,
				Errors = errorMessages
			};
		}

		public async Task<bool> UpdateAssetAsync(AssetUpdateDto assetUpdateDto)
		{
			var validationResult = await _validatorAssetUpdate.ValidateAsync(assetUpdateDto);
			if (!validationResult.IsValid)
			{
				var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
				throw new ValidationException($"Validation failed: {string.Join("; ", errorMessages)}");
			}
			Asset asset = _mapper.Map<Asset>(assetUpdateDto);
			return await _assetRepository.UpdateAssetAsync(asset);
		}

		public async Task<bool> DeleteAssetAsync(Guid assetId)
		{
			return await _assetRepository.DeleteAssetAsync(assetId);
		}
	}
}
