using System.Text.Json;
using BrightInvest.Application.DTOs.AlphaVantage;
using BrightInvest.Domain.Entities;

namespace BrightInvest.Application.Services.AlphaVantage
{
	public class AlphaVantageService : IAlphaVantageService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;
		private readonly string _baseUrl;

		public AlphaVantageService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_apiKey = configuration["AlphaVantage:ApiKey"] ?? throw new InvalidOperationException("Alpha Vantage API Key is missing.");
			_baseUrl = configuration["AlphaVantage:BaseUrl"] ?? throw new InvalidOperationException("Alpha Vantage BaseUrl is missing.");
		}


		public async Task<List<StockDataDto>> FetchAllStockPricesAsync(string symbol)
		{
			var url = $"{_baseUrl}/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}";

			var response = await _httpClient.GetStringAsync(url);

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
			return stockData.TimeSeries
				.Select(ts => new StockDataDto
				{
					Date = DateTime.Parse(ts.Key),
					ClosePrice = ts.Value.ClosePrice
				})
				.ToList();

		}
	}
}

