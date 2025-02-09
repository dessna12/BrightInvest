using OfficeOpenXml;

namespace BrightInvest.Application.Services.Excel
{
	public class ExcelService
	{
		public async Task<List<AssetCreateDto>> ReadAssetsFromExcelAsync(Stream fileStream)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Required for EPPlus

			// Create a list to store assets
			var assets = new List<AssetCreateDto>();

			// Read the stream asynchronously
			using (var package = new ExcelPackage())
			{
				await package.LoadAsync(fileStream);

				var worksheet = package.Workbook.Worksheets[0]; // Assuming first sheet contains data
				var rowCount = worksheet.Dimension.Rows;

				for (int row = 2; row <= rowCount; row++) // Assuming row 1 is headers
				{
					// Create AssetCreateDto for each row
					var asset = new AssetCreateDto
					(
						worksheet.Cells[row, 1].Text,  // Ticker
						worksheet.Cells[row, 2].Text,  // Name
						worksheet.Cells[row, 3].Text,  // Currency
						worksheet.Cells[row, 4].Text,  // Country
						worksheet.Cells[row, 5].Text   // Sector
					);
					assets.Add(asset);
				}
			}

			return assets;
		}
	}
}
