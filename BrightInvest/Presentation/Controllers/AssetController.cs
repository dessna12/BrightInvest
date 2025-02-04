using System.Collections.Generic;
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
			var assets = await _assetUseCase.GetAllAssetsAsync();
			return Ok(assets);
		}

		// GET: api/assets/id
		[HttpGet("{id}")]
		public async Task<ActionResult<AssetDto>> GetAsset(Guid id)
		{
			var asset = await _assetUseCase.GetAssetByIdAsync(id);
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
				return BadRequest("Invalid asset data");

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _assetUseCase.CreateAssetAsync(asset);
			return Ok(asset);
		}

		//DELETE: api/assets/id
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsset(Guid id)
		{
			bool response = await _assetUseCase.DeleteAssetAsync(id);

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
