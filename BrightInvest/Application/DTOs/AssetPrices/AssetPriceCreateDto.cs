namespace BrightInvest.Application.DTOs.AssetPrices
{
	public record AssetPriceCreateDto(Guid AssetId, DateTime Date, decimal ClosePrice);
}
