using BrightInvest.Application.DTOs.Currency;
using BrightInvest.Application.UseCases.Interfaces;
using BrightInvest.Domain.Enum;
using BrightInvest.Domain.Interfaces;

namespace BrightInvest.Application.UseCases.Currencies
{
	public class CurrencyUseCase : ICurrencyUseCase
	{

		private readonly ICurrencyRepository _currencyRepository;
		public CurrencyUseCase(ICurrencyRepository currencyRepository) {
			_currencyRepository = currencyRepository;
		}

		public List<CurrencyDto> GetAvailableCurrencies()
		{
			var currencies = _currencyRepository.GetAvailableCurrencies();

			var currencyDtos = currencies.Select(c => new CurrencyDto
			{
				Name = c.ToString(),
				Symbol = GetCurrencySymbol(c)
			}).ToList();

			return currencyDtos;
		}



		private string GetCurrencySymbol(Currency currency)
		{
			return currency switch
			{
				Currency.USD => "$",
				Currency.EUR => "€",
				Currency.GBP => "£",
				Currency.JPY => "¥",
				_ => currency.ToString()
			};
		}


	}
}
