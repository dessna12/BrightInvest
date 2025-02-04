using BrightInvest.Domain.Enum;

namespace BrightInvest.Domain.Interfaces
{
	public interface ICurrencyRepository
	{
		List<Currency> GetAvailableCurrencies();

	}
}
