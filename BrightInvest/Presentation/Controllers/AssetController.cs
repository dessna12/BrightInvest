using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using BrightInvest.Application.Services.Assets;
using BrightInvest.Application.UseCases.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrightInvest.Presentation.Controllers
{
	[Route("assets")]
	[ApiController]
	public class AssetController : Controller
	{

		private readonly IAssetUseCase _assetUseCase;

		public AssetController(IAssetUseCase assetUseCase)
		{
			_assetUseCase = assetUseCase ?? throw new ArgumentNullException(nameof(assetUseCase));
		}

		// GET: api/assets/
		[HttpGet]
		public async Task<IActionResult> GetAssets()
		{
			try
			{
				var assets = await _assetUseCase.GetAllAssetsAsync();
				return Ok(assets);
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

		// GET: api/assets/id
		[HttpGet("{id}")]
		public async Task<ActionResult<AssetDto>> GetAsset(Guid id)
		{
			try
			{
				var asset = await _assetUseCase.GetAssetByIdAsync(id);
				return Ok(asset);
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

		//POST: api/assets
		[HttpPost]
		public async Task<ActionResult<AssetDto>> PostAsset([FromBody] AssetCreateDto asset)
		{
			if (asset == null)
				return BadRequest("Invalid asset data");
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				await _assetUseCase.CreateAssetAsync(asset);
				return Ok(asset);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
			}
		}

		//PUT: api/assets/id
		[HttpPut("{id}")]
		public async Task<ActionResult<AssetDto>> UpdateAsset(Guid id, [FromBody] AssetUpdateDto asset)
		{
			if (asset == null)
				return BadRequest("Invalid asset data");
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				await _assetUseCase.UpdateAssetAsync(asset);
				return Ok(asset);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
			}
		}


		//DELETE: api/assets/id
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsset(Guid id)
		{
			try
			{
				bool response = await _assetUseCase.DeleteAssetAsync(id);
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
