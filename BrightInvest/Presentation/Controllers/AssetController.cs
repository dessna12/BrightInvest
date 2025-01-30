using System.Net;
using BrightInvest.Domain.Entities;
using BrightInvest.Infrastructure.DataBase;
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


		// GET: api/asset/id
		[HttpGet]
		[Route("assets")]
		public IActionResult GetAssets()
		{
			List<Asset> assets = _context.Assets.ToList();

			return Json(assets);
		}

		// GET: api/asset
		//[HttpGet]
		//[Route("assets")]
		//public IActionResult GetAssets()
		//{
		//	List<Asset> assets = _context.Assets.ToList();

		//	return Json(assets);
		//}


	}
}
