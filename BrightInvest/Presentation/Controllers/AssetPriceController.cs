using System.Collections.Generic;
using System.Net;
using BrightInvest.Application.DTOs.AssetPrices;
using BrightInvest.Application.Services;
using BrightInvest.Application.Services.AssetPrices;
using BrightInvest.Application.UseCases.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrightInvest.Presentation.Controllers
{
	[Route("asset-prices")]
	[ApiController]
	public class AssetPriceController : Controller
	{

		private readonly IAssetPriceUseCase _assetPriceUseCase;

		public AssetPriceController(IAssetPriceUseCase assetPriceUseCase)
		{
			_assetPriceUseCase = assetPriceUseCase ?? throw new ArgumentNullException(nameof(assetPriceUseCase));
		}

		// GET: api/asset-prices/
		[HttpGet]
		public async Task<IActionResult> GetAssets()
		{
			var assetPrices = await _assetPriceUseCase.GetAllAssetPricesAsync();
			return Ok(assetPrices);
		}

		// GET: api/asset-prices/id
		[HttpGet("{id}")]
		public async Task<ActionResult<AssetPriceDto>> GetAssetPrice(Guid id)
		{
			var assetPrice = await _assetPriceUseCase.GetAssetPriceByIdAsync(id);
			if (assetPrice == null)
			{
				return NotFound();
			}
			return Ok(assetPrice);
		}

		//POST: api/asset-prices
		[HttpPost]
		public async Task<ActionResult<AssetPriceDto>> PostAssetPrice([FromBody] AssetPriceCreateDto assetPrice)
		{
			if (assetPrice == null)
			{
				return BadRequest("Invalid asset data");
			}

			await _assetPriceUseCase.CreateAssetPriceAsync(assetPrice);
			return Ok(assetPrice);
		}

		//DELETE: api/asset-prices/id
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsset(Guid id)
		{
			bool response = await _assetPriceUseCase.DeleteAssetPriceAsync(id);

			if (response)
			{
				return NoContent();
			}
			else
			{
				return NotFound(); 
			}
		}

	}
}
