using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using BrightInvest.Domain.Primitives;

namespace BrightInvest.Domain.Entities;
public class Asset : Entity
{
	public string Ticker {  get; set; }
	public string Name { get; set; }
	public List<AssetPrice> Prices { get; private set; } = new();


	[JsonConstructor]
	public Asset(Guid id, string ticker, string name ) : base(id)
	{
		Ticker = ticker;
		Name = name;
	}

	public Asset(string ticker, string name) : this(Guid.NewGuid(), ticker, name)
	{
	}
}
