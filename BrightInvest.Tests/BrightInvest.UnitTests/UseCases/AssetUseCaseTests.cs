using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BrightInvest.Application.DTOs.Assets;
using BrightInvest.Application.UseCases.Assets;
using BrightInvest.Domain.Entities;
using BrightInvest.Domain.Interfaces;
using BrightInvest.Domain.Enum;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;
using FluentAssertions;

public class AssetUseCaseTests
{

	// Mapping Logic - Mapper configuration
	private IMapper CreateMapper()
	{
		var config = new MapperConfiguration(cfg =>
		{
			cfg.CreateMap<Asset, AssetDto>()
				.ForMember(dest => dest.Currency, opt => opt.MapFrom(src => MapCurrencyToString(src.Currency)))
				.ForMember(dest => dest.Sector, opt => opt.MapFrom(src => MapSectorToString(src.Sector)))
				.ForMember(dest => dest.Country, opt => opt.MapFrom(src => MapCountryToString(src.Country)));
		});
		return config.CreateMapper();
	}

	// Custom mapping logic
	private string MapCurrencyToString(Currency currency)
	{
		return currency.ToString();
	}

	private string MapSectorToString(Sector sector)
	{
		return sector.ToString();
	}

	private string MapCountryToString(Country country)
	{
		return country.ToString();
	}


	[Fact]
	public void GetAllAssetsAsync_ReturnsAssets()
	{
		// Arrange
		var mockAssetRepo = new Mock<IAssetRepository>();
		mockAssetRepo.Setup(repo => repo.GetAllAssetsAsync()).ReturnsAsync(new List<Asset> {
			new Asset
			{
				Ticker = "AAPL",
				Name = "Apple Inc.",
				Currency = Currency.USD,
				Country = Country.UnitedStates,
				Sector = Sector.Technology
			}
		});

		var mapper = CreateMapper();
		var assetUseCase = new AssetUseCase(mockAssetRepo.Object, mapper, null, null);

		// Act
		var result = assetUseCase.GetAllAssetsAsync().Result;

		// Assert
		result.Should().NotBeNull();
		result.Should().HaveCount(1);
		var assetDto = result.First();
		assetDto.Ticker.Should().Be("AAPL");
		assetDto.Name.Should().Be("Apple Inc.");
		assetDto.Currency.Should().Be("USD");
		assetDto.Country.Should().Be("UnitedStates");
		assetDto.Sector.Should().Be("Technology");
	}

}
