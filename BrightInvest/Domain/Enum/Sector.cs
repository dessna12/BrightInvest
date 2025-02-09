using System.ComponentModel;

namespace BrightInvest.Domain.Enum
{
	public enum Sector
	{
		[Description("Financials")]
		Financials,

		[Description("Healthcare")]
		Healthcare,

		[Description("Technology")]
		Technology,

		[Description("Industrials")]
		Industrials,

		[Description("Consumer Discretionary")]
		ConsumerDiscretionary,

		[Description("Materials")]
		Materials,

		[Description("Real Estate")]
		RealEstate,

		[Description("Energy")]
		Energy,

		[Description("Communication Services")]
		CommunicationServices,

		[Description("Consumer Staples")]
		ConsumerStaples,

		[Description("Utilities")]
		Utilities
	}
}
