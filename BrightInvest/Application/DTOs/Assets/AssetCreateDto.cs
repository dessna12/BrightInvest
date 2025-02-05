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
	public Currency Currency { get; set; }

	public AssetCreateDto(string ticker, string name, Currency currency)
	{
		Ticker = ticker;
		Name = name;
		Currency = currency;
	}
}