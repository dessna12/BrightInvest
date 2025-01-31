public record AssetDto
{
	public Guid Id { get; init; }
	public string Ticker { get; init; }
	public string Name { get; init; }

	public AssetDto(Guid id, string ticker,  string name)
	{
		Id = id;
		Ticker = ticker;
		Name = name;
	}
}