using BrightInvest.Application.DTOs.AlphaVantage;
using BrightInvest.Domain.Entities;

namespace BrightInvest.Application.Services.AlphaVantage
{
	public interface IAlphaVantageService
	{
		Task<List<StockDataDto>> FetchAllStockPricesAsync(string symbol);

	}
}
