namespace BrightInvest.Application.DTOs.AssetPrices
{
	public record AssetPriceDto(Guid Id, Guid AssetId, DateTime Date, decimal ClosePrice);
}
