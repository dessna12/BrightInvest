using System.Net;
using BrightInvest.Domain.Entities;
using BrightInvest.Infrastructure.DataBase;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrightInvest.Presentation.Controllers
{
	[Route("api/")]
	[ApiController]
	public class AssetController : Controller
	{

		private readonly DataContext _context;

		public AssetController(DataContext context)
		{
			_context = context;
		}


		// GET: api/asset
		[HttpGet]
		[Route("/assets")]
		public IEnumerable<string> Get()
		{
			//List<Asset> assets = _context.Assets.ToList();
			return new string[] { "value1", "value2" };

			//return Json(assets);
		}

	}
}
