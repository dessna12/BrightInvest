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
			try
			{
				var assetPrices = await _assetPriceUseCase.GetAllAssetPricesAsync();
				return Ok(assetPrices);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
			}
		}

		// GET: api/asset-prices/id
		[HttpGet("{id}")]
		public async Task<ActionResult<AssetPriceDto>> GetAssetPrice(Guid id)
		{
			try
			{
				var assetPrice = await _assetPriceUseCase.GetAssetPriceByIdAsync(id);
				return Ok(assetPrice);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
			}
		}

		// GET: api/asset-prices/ticker/symbol
		[HttpGet("ticker/{symbol}")]
		public async Task<ActionResult<AssetPriceDto>> GetAllAssetPricesBySymbolAsync(string symbol)
		{
			try
			{
				var assetPrices = await _assetPriceUseCase.GetAllAssetPricesBySymbolAsync(symbol);
				return Ok(assetPrices);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
			}
		}


		//POST: api/asset-prices
		[HttpPost]
		public async Task<ActionResult<AssetPriceDto>> PostAssetPrice([FromBody] AssetPriceCreateDto assetPrice)
		{
			if (assetPrice == null)
				return BadRequest("Invalid asset data");
			try
			{
				await _assetPriceUseCase.CreateAssetPriceAsync(assetPrice);
				return Ok(assetPrice);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
			}
		}

		//DELETE: api/asset-prices/id
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsset(Guid id)
		{
			try
			{
				bool response = await _assetPriceUseCase.DeleteAssetPriceAsync(id);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
			}
		}

	}
}
