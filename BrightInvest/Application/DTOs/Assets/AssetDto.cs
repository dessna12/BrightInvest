using BrightInvest.Domain.Enum;

public record AssetDto
{
	public Guid Id { get; init; }
	public string Ticker { get; init; }
	public string Name { get; init; }
	public string Currency { get; init; }
	//public Currency Currency { get; init; }
	public string Country { get; init; }
	public string Sector { get; init; }


	public AssetDto(Guid id, string ticker,  string name, string currency, string country, string sector)
	{
		Id = id;
		Ticker = ticker;
		Name = name;
		Currency = currency;
		Country = country;
		Sector = sector;
	}
}