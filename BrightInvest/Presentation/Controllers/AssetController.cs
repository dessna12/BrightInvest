using System.Net;
using BrightInvest.Domain.Entities;
using BrightInvest.Infrastructure.DataBase;
using BrightInvest.Web.Pages;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrightInvest.Presentation.Controllers
{
	//[Route("/api")]
	[ApiController]
	public class AssetController : Controller
	{

		private readonly DataContext _context;

		public AssetController(DataContext context)
		{
			_context = context;
		}


		// GET: api/asset/
		[HttpGet]
		[Route("assets")]
		public IActionResult GetAssets()
		{
			List<Asset> assets = _context.Assets.ToList();

			return Json(assets);
		}

		// GET: api/asset
		[HttpGet("{id}")]
		public async Task<ActionResult<Asset>> GetAsset(Guid id)
		{
			var asset = await _context.Assets.FindAsync(id);
			if (asset == null)
			{
				return NotFound();
			}
			return asset;
		}

		//POST: api/asset
		[HttpPost]
		[Route("asset")]
		public async Task<ActionResult<Asset>> PostAsset([FromBody] Asset asset) {
			if (asset == null)
			{
				return BadRequest("Invalid asset data");
			}

			_context.Assets.Add(asset);
			await _context.SaveChangesAsync();

			return Ok(asset);
		}



	}
}
