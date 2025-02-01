namespace BrightInvest.Application.DTOs.AssetPrice
{

	public record AssetPriceCreateDto(Guid AssetId, DateTime Date, decimal ClosePrice);
}
