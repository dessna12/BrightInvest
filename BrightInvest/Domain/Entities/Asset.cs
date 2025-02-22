using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using BrightInvest.Domain.Enum;
using BrightInvest.Domain.Primitives;

namespace BrightInvest.Domain.Entities;
public class Asset : Entity
{
	public string Ticker {  get; set; }
	public string Name { get; set; }
	public Currency Currency { get; set; }
	public Country Country { get; set; }
	public Sector Sector { get; set; }
	public List<AssetPrice> Prices { get; private set; } = new();


	[JsonConstructor]
	public Asset(Guid id, string ticker, string name, Currency currency, Country country, Sector sector) : base(id)
	{
		Ticker = ticker;
		Name = name;
		Currency = currency;
		Country = country;
		Sector = sector;
	}

	public Asset(string ticker, string name, Currency currency, Country country, Sector sector) : this(Guid.NewGuid(), ticker, name, currency, country, sector )
	{
	}



}
