namespace BrightInvest.Application.DTOs.Assets
{
	public class AssetsCreateResponseDto
	{
		public List<AssetDto> Success { get; set; }
		public List<string> Errors { get; set; }

		public AssetsCreateResponseDto()
		{
			Success = new List<AssetDto>();
			Errors = new List<string>();
		}
	}
}
