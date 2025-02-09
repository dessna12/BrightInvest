using OfficeOpenXml;

namespace BrightInvest.Application.Services.Excel
{
	public class ExcelService
	{
		public async Task<List<AssetDto>> ReadAssetsFromExcelAsync(Stream fileStream)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Required for EPPlus

			using var package = new ExcelPackage(fileStream);
			var worksheet = package.Workbook.Worksheets[0]; // Assuming first sheet contains data
			var rowCount = worksheet.Dimension.Rows;
			var assets = new List<AssetDto>();

			for (int row = 2; row <= rowCount; row++) // Assuming row 1 is headers
			{
				var asset = new AssetDto
				(
					Guid.NewGuid(),
					worksheet.Cells[row, 1].Text,  // Ticker
					worksheet.Cells[row, 2].Text,  // Name
					worksheet.Cells[row, 3].Text,  // Currency
					worksheet.Cells[row, 4].Text,  // Country
					worksheet.Cells[row, 5].Text   // Sector

				);
				assets.Add(asset);
			}

			return assets;
		}

	}
}
