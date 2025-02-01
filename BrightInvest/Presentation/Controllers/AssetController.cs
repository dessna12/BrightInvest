using System.Collections.Generic;
using System.Net;
using BrightInvest.Application.Services;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrightInvest.Presentation.Controllers
{
	[Route("assets")]
	[ApiController]
	public class AssetController : Controller
	{

		private readonly IAssetService _assetService;

		public AssetController(IAssetService assetService)
		{
			_assetService = assetService ?? throw new ArgumentNullException(nameof(assetService));
		}

		// GET: api/assets/
		[HttpGet]
		public async Task<IActionResult> GetAssets()
		{
			var assets = await _assetService.GetAllAssetsAsync();
			return Ok(assets);
		}

		// GET: api/assets/id
		[HttpGet("{id}")]
		public async Task<ActionResult<AssetDto>> GetAsset(Guid id)
		{
			var asset = await _assetService.GetAssetByIdAsync(id);
			if (asset == null)
			{
				return NotFound();
			}
			return Ok(asset);
		}

		//POST: api/assets
		[HttpPost]
		public async Task<ActionResult<AssetDto>> PostAsset([FromBody] AssetCreateDto asset)
		{
			if (asset == null)
			{
				return BadRequest("Invalid asset data");
			}

			await _assetService.CreateAssetAsync(asset);
			return Ok(asset);
		}

		//DELETE: api/assets/id
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsset(Guid id)
		{
			bool response = await _assetService.DeleteAssetAsync(id);

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
