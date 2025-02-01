using System.Collections.Generic;
using System.Net;
using BrightInvest.Application.DTOs.AssetPrice;
using BrightInvest.Application.Services;
using BrightInvest.Application.Services.AssetPrice;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrightInvest.Presentation.Controllers
{
	[Route("asset-prices")]
	[ApiController]
	public class AssetPriceController : Controller
	{

		private readonly IAssetPriceService _assetPriceService;

		public AssetPriceController(IAssetPriceService assetPriceService)
		{
			_assetPriceService = assetPriceService ?? throw new ArgumentNullException(nameof(assetPriceService));
		}

		// GET: api/asset-prices/
		[HttpGet]
		public async Task<IActionResult> GetAssets()
		{
			var assetPrices = await _assetPriceService.GetAllAssetPricesAsync();
			return Ok(assetPrices);
		}

		// GET: api/asset-prices/id
		[HttpGet("{id}")]
		public async Task<ActionResult<AssetPriceDto>> GetAssetPrice(Guid id)
		{
			var assetPrice = await _assetPriceService.GetAssetPriceByIdAsync(id);
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

			await _assetPriceService.CreateAssetPriceAsync(assetPrice);
			return Ok(assetPrice);
		}

		//DELETE: api/asset-prices/id
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsset(Guid id)
		{
			bool response = await _assetPriceService.DeleteAssetPriceAsync(id);

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
