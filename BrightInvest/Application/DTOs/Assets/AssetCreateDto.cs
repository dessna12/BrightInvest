using BrightInvest.Domain.Enum;

public class AssetCreateDto
{
	public string Ticker { get; set; }
	public string Name { get; set; }
	public Currency Currency { get; set; }


	public AssetCreateDto(string ticker, string name, Currency currency)
	{
		Ticker = ticker;
		Name = name;
		Currency = currency;
	}
}