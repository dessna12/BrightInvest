public class AssetCreateDto
{
	public string Ticker { get; set; }
	public string Name { get; set; }


	public AssetCreateDto(string ticker, string name)
	{
		Ticker = ticker;
		Name = name;
	}
}