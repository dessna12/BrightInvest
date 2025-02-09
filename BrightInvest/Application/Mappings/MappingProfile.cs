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
					opt => opt.MapFrom(src => MapCurrencyToString(src.Currency)))  // Custom logic to convert Enum to string
				.ForMember(dest => dest.Sector,
					opt => opt.MapFrom(src => MapSectorToString(src.Sector)))  // Convert Sector Enum to string
				.ForMember(dest => dest.Country,
					opt => opt.MapFrom(src => MapCountryToString(src.Country)));  // Convert Country Enum to string

			CreateMap<AssetUpdateDto, Asset>()
				.ForMember(dest => dest.Currency,
					opt => opt.MapFrom(src => MapCurrency(src.Currency)))
				.ForMember(dest => dest.Sector,
					opt => opt.MapFrom(src => MapSector(src.Sector)))  // Convert string to Sector Enum
				.ForMember(dest => dest.Country,
					opt => opt.MapFrom(src => MapCountry(src.Country)));  // Convert string to Country Enum

			CreateMap<AssetCreateDto, Asset>()
				.ForMember(dest => dest.Currency,
					opt => opt.MapFrom(src => MapCurrency(src.Currency)))
				.ForMember(dest => dest.Sector,
					opt => opt.MapFrom(src => MapSector(src.Sector)))  // Convert string to Sector Enum
				.ForMember(dest => dest.Country,
					opt => opt.MapFrom(src => MapCountry(src.Country)));  // Convert string to Country Enum
		}

		// Method to convert string to Currency Enum
		private Currency MapCurrency(string currency)
		{
			return Enum.TryParse<Currency>(currency, out var result) ? result : default;
		}

		// Method to convert Enum to string for Currency
		private string MapCurrencyToString(Currency currency)
		{
			return currency.ToString();
		}

		// Method to convert string to Sector Enum (null-safe)
		private Sector? MapSector(string? sector)
		{
			return string.IsNullOrEmpty(sector) ? null : Enum.TryParse<Sector>(sector, out var result) ? result : null;
		}

		// Method to convert Enum to string for Sector
		private string MapSectorToString(Sector? sector)
		{
			return sector?.ToString() ?? string.Empty;  // If null, return empty string or another placeholder as needed
		}

		// Method to convert string to Country Enum
		private Country MapCountry(string country)
		{
			return Enum.TryParse<Country>(country, out var result) ? result : default;
		}

		// Method to convert Enum to string for Country
		private string MapCountryToString(Country country)
		{
			return country.ToString();
		}
	}
}