﻿using BrightInvest.Domain.Entities;

namespace BrightInvest.Application.UseCases.Assets
{
	public interface IAssetUseCase
	{
		Task<IEnumerable<AssetDto?>> GetAllAssetsAsync();
		Task<AssetDto> GetAssetByIdAsync(Guid id);
		Task<AssetDto> CreateAssetAsync(AssetCreateDto assetCreateDto);
		Task<bool> DeleteAssetAsync(Guid assetId);
	}
}
