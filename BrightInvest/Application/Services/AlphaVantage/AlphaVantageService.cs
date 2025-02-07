using System.Text.Json;
using BrightInvest.Application.DTOs.AlphaVantage;
using BrightInvest.Domain.Entities;
using BrightInvest.Web.Services;

namespace BrightInvest.Application.Services.AlphaVantage
{
	public class AlphaVantageService : IAlphaVantageService
	{
		private readonly CustomHttpClientService _customHttpClientService;
		private readonly string _apiKey;
		private readonly string _baseUrl;

		public AlphaVantageService(CustomHttpClientService customHttpClientService, IConfiguration configuration)
		{
			_customHttpClientService = customHttpClientService;
			_apiKey = configuration["AlphaVantage:ApiKey"] ?? throw new InvalidOperationException("Alpha Vantage API Key is missing.");
			_baseUrl = configuration["AlphaVantage:BaseUrl"] ?? throw new InvalidOperationException("Alpha Vantage BaseUrl is missing.");
		}


		public async Task<List<StockDataDto>> FetchAllStockPricesAsync(string symbol, DateTime? fromDate = null)
		{
			var url = $"{_baseUrl}/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}";

			var httpClient = _customHttpClientService.GetHttpClient();
			var response = await httpClient.GetStringAsync(url);

			if (string.IsNullOrWhiteSpace(response))
			{
				throw new Exception($"Error fetching data for symbol {symbol}");
			}

			var stockData = JsonSerializer.Deserialize<AlphaVantageResponseDto>(response);

			if (stockData?.TimeSeries == null)
			{
				throw new Exception($"No time series data available for symbol {symbol}");
			}

			// Map AlphaVantage response to StockDataDto
			List<StockDataDto> stockDataDto = stockData.TimeSeries
				.Select(ts => new StockDataDto
				{
					Date = DateTime.Parse(ts.Key),
					ClosePrice = ParseClosePrice(ts.Value.ClosePrice)
				})
				.ToList();

			if (fromDate != null)
				stockDataDto = stockDataDto.Where(data => data.Date >= fromDate).ToList();

			return stockDataDto;
		}

		private decimal ParseClosePrice(string closePrice)
		{
			if (decimal.TryParse(closePrice, out var parsedValue))
			{
				return parsedValue;
			}

			throw new Exception($"Failed to parse ClosePrice value: {closePrice}");
		}
	}
}

