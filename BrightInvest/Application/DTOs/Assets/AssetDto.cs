using System.ComponentModel.DataAnnotations;
using BrightInvest.Domain.Enum;

public record AssetDto
{
	public Guid Id { get; init; }
	public string Ticker { get; init; }
	public string Name { get; init; }
	public Currency Currency { get; init; }

	public AssetDto(Guid id, string ticker,  string name, Currency currency)
	{
		Id = id;
		Ticker = ticker;
		Name = name;
		Currency = currency;
	}
}