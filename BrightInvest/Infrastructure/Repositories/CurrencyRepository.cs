using BrightInvest.Domain.Enum;
using BrightInvest.Domain.Interfaces;

namespace BrightInvest.Infrastructure.Repositories
{
	public class CurrencyRepository : ICurrencyRepository
	{
		public List<Currency> GetAvailableCurrencies()
		{
			return Enum.GetValues(typeof(Currency)).Cast<Currency>().ToList();
		}
	}
}
