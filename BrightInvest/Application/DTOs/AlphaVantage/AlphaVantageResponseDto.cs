using System.Text.Json.Serialization;
using BrightInvest.Domain.Entities;

namespace BrightInvest.Application.DTOs.AlphaVantage
	
{
	public class AlphaVantageResponseDto
	{
		[JsonPropertyName("Time Series (Daily)")]
		public Dictionary<string, TimeSeriesData> TimeSeries { get; set; }
	}

	public class TimeSeriesData
	{
		[JsonPropertyName("4. close")]
		public decimal ClosePrice { get; set; }
	}

	public class StockDataDto
	{
		public DateTime Date { get; set; }
		public decimal ClosePrice { get; set; }
	}
}
