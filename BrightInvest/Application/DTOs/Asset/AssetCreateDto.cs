public record AssetCreateDto
{
	public string Ticker { get; init; }
	public string Name { get; init; }

	public AssetCreateDto(string ticker, string name)
	{
		Ticker = ticker;
		Name = name;
	}
}