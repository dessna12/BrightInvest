using System.ComponentModel.DataAnnotations;
using BrightInvest.Domain.Enum;

public class AssetCreateDto
{
	[Required]
	[StringLength(10, MinimumLength = 2)]
	public string Ticker { get; set; }

	[Required]
	[StringLength(50, MinimumLength = 2)]
	public string Name { get; set; }

	[Required]
	public string Currency { get; set; }

	[Required]
	public string Country { get; set; }

	public string? Sector { get; set; }

	public AssetCreateDto(string ticker, string name, string currency, string country, string? sector)
	{
		Ticker = ticker;
		Name = name;
		Currency = currency;
		Country = country;
		Sector = sector;
	}
}