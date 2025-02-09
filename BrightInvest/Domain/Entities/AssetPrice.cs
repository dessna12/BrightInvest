using System.Text.Json.Serialization;
using BrightInvest.Domain.Primitives;

namespace BrightInvest.Domain.Entities
{
	public class AssetPrice : Entity
	{
		public Guid AssetId { get; private set; }  // Foreign Key
		public Asset Asset { get; private set; }   // Navigation Property
		public DateTime Date { get; set; }
		public decimal ClosePrice { get; set; }


		[JsonConstructor]
		public AssetPrice(Guid id, Guid assetId, DateTime date, decimal closePrice) : base(id)
		{ 
			AssetId = assetId;
			Date= date;
			ClosePrice= closePrice;
		}

		public AssetPrice(Guid assetId, DateTime date, decimal closePrice) : this(Guid.NewGuid(), assetId, date, closePrice)
		{ 
		}


	}
}
