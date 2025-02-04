using System.Collections.Generic;
using System.Net;
using BrightInvest.Application.UseCases.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Mvc;


namespace BrightInvest.Presentation.Controllers
{
	[Route("currencies")]
	[ApiController]
	public class CurrencyController : Controller
	{

		private readonly ICurrencyUseCase _currencyUseCase;

		public CurrencyController(ICurrencyUseCase currencyUseCase)
		{
			_currencyUseCase = currencyUseCase ?? throw new ArgumentNullException(nameof(currencyUseCase));
		}

		// GET: api/currencies
		[HttpGet]
		public IActionResult GetCurrencies()
		{
			var currencies = _currencyUseCase.GetAvailableCurrencies();
			return Ok(currencies);
		}

	}
}
