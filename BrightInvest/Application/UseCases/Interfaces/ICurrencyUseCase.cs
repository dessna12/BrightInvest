using BrightInvest.Application.DTOs.Currency;
using BrightInvest.Domain.Enum;

namespace BrightInvest.Application.UseCases.Interfaces
{
	public interface ICurrencyUseCase
	{
		List<CurrencyDto> GetAvailableCurrencies();
	}
}
