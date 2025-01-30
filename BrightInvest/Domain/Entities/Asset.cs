using System;
using System.Security.Cryptography.X509Certificates;
using BrightInvest.Domain.Primitives;

namespace BrightInvest.Domain.Entities;
public class Asset : Entity
{
	public Guid AssetID { get; set; }
	public string Ticker {  get; set; }
	public string Name { get; set; }


	public Asset(string ticker, string name ) : base(Guid.NewGuid())
	{
		AssetID = Guid.NewGuid();
		Ticker = ticker;
		Name = name;
	}
}
