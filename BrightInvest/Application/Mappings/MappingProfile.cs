using AutoMapper;
using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Enum;

namespace BrightInvest.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Asset, AssetDto>()
				.ForMember(dest => dest.Currency,
					opt => opt.MapFrom(src => MapCurrencyToString(src.Currency)));  // Custom logic to convert Enum to string


			CreateMap<AssetUpdateDto, Asset>()
				.ForMember(dest => dest.Currency,
					opt => opt.MapFrom(src => MapCurrency(src.Currency)));

			CreateMap<AssetCreateDto, Asset>()
				.ForMember(dest => dest.Currency,
					opt => opt.MapFrom(src => MapCurrency(src.Currency)));
		}



		// Method to convert string to Enum
		private Currency MapCurrency(string currency)
		{
			return Enum.TryParse<Currency>(currency, out var result) ? result : default;
		}

		// Method to convert Enum to string
		private string MapCurrencyToString(Currency currency)
		{
			return currency.ToString();
		}
	}
}
