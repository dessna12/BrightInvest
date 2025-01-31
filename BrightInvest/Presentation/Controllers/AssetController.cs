using System.Collections.Generic;
using System.Net;
using BrightInvest.Application.Services;
using BrightInvest.Domain.Entities;
using BrightInvest.Web.Pages;
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
			var assets = await _assetService.GetAssetsAsync();

			return Ok(assets);
		}

		//// GET: api/assets/id
		//[HttpGet("{id}")]
		//public async Task<ActionResult<Asset>> GetAsset(Guid id)
		//{
		//	var asset = await _context.Assets.FindAsync(id);
		//	if (asset == null)
		//	{
		//		return NotFound();
		//	}
		//	return asset;
		//}

		////POST: api/assets
		//[HttpPost]
		//public async Task<ActionResult<Asset>> PostAsset([FromBody] Asset asset) {
		//	if (asset == null)
		//	{
		//		return BadRequest("Invalid asset data");
		//	}

		//	_context.Assets.Add(asset);
		//	await _context.SaveChangesAsync();

		//	return Ok(asset);
		//}

		////DELETE: api/assets/id
		//[HttpDelete("{id}")]
		//public async Task<IActionResult> DeleteAsset(Guid id)
		//{
		//	var asset = await _context.Assets.FindAsync(id);
		//	if (asset == null)
		//	{
		//		return NotFound($"Asset with ID {id} not found");
		//	}

		//	_context.Assets.Remove(asset);
		//	await _context.SaveChangesAsync();

		//	return NoContent(); // 204 No Content is the standard response for DELETE
		//}





	}
}
