using System.ComponentModel.DataAnnotations;
using BrightInvest.Domain.Enum;

public class AssetUpdateDto
{
	public Guid Id { get; set; }

	[Required]
	[StringLength(10, MinimumLength = 2)]
	public string Ticker { get; set; }

	[Required]
	[StringLength(50, MinimumLength = 2)]
	public string Name { get; set; }

	[Required]
	public Currency Currency { get; set; }

	public AssetUpdateDto(Guid id, string ticker, string name, Currency currency)
	{
		Id = id;
		Ticker = ticker;
		Name = name;
		Currency = currency;
	}
}