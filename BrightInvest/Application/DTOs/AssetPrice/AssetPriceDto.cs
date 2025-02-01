namespace BrightInvest.Application.DTOs.AssetPrice
{
	public record AssetPriceDto(Guid Id, Guid AssetId, DateTime Date, decimal ClosePrice);
}
