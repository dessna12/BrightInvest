using BrightInvest.Domain.Enum;
using FluentValidation;

public class AssetUpdateDtoValidator : AbstractValidator<AssetUpdateDto>
{
	public AssetUpdateDtoValidator()
	{
		RuleFor(a => a.Id)
			.NotEmpty().WithMessage("Id is required.")
			.Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Id must be a valid GUID.");

		RuleFor(a => a.Ticker)
			.NotEmpty().WithMessage("Symbol is required.")
			.MaximumLength(10).WithMessage("Symbol cannot exceed 10 characters.");

		RuleFor(a => a.Name)
			.NotEmpty().WithMessage("Name is required.");

		RuleFor(a => a.Currency)
			.Must(BeValidCurrency).WithMessage(a => $"Invalid currency: {a.Currency}");

		RuleFor(a => a.Sector)
			.Must(BeValidSector).When(a => !string.IsNullOrEmpty(a.Sector))
			.WithMessage(a => $"Invalid sector: {a.Sector}");

		RuleFor(a => a.Country)
			.Must(BeValidCountry).WithMessage(a => $"Invalid country: {a.Country}");
	}

	private bool BeValidCurrency(string currency)
		=> Enum.TryParse<Currency>(currency, out _);

	private bool BeValidSector(string sector)
		=> Enum.TryParse<Sector>(sector, out _);

	private bool BeValidCountry(string country)
		=> Enum.TryParse<Country>(country, out _);
}