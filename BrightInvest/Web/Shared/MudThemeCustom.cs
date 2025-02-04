using MudBlazor;

public static class CustomMudTheme
{
	public static MudTheme GetTheme() => new MudTheme()
	{
		PaletteLight = new PaletteLight()
		{
			Primary = "#339899",
			Secondary = "#5abab9",
			Tertiary = "#abe4e0",
			Background = "#f5f5f5",
			Surface = "#f5f5f5",
			AppbarBackground = "#204d4f",
			DrawerBackground = "#204d4f",
			DrawerText = "#f2fbfa",
			Success = "#78d0cc",
			Warning = "#d5f2ef",
			Error = "#d8390d",
			Info = "#339899",
			TextPrimary = "#1e4243",
			TextSecondary = "#226163",
			ActionDefault = "#339899",
			ActionDisabled = "#d5f2ef",
			ActionDisabledBackground = "#abe4e0"
		},
		Typography = new Typography()
		{
			H1 = new H1Typography()
			{
				FontFamily = new[] { "Pathway Gothic One" },
				FontSize = "2.25rem",
				FontWeight = "700",
				LineHeight = "1.3",
				LetterSpacing = "0em"
			},
			H2 = new H2Typography()
			{
				FontFamily = new[] { "Pathway Gothic One" },
				FontSize = "2rem",
				FontWeight = "700",
				LineHeight = "1.35",
				LetterSpacing = "0em"
			},
			H3 = new H3Typography()
			{
				FontFamily = new[] { "Pathway Gothic One" },
				FontSize = "1.75rem",
				FontWeight = "600",
				LineHeight = "1.4",
				LetterSpacing = "0.0075em"
			},
			H4 = new H4Typography()
			{
				FontFamily = new[] { "Pathway Gothic One" },
				FontSize = "1.5rem",
				FontWeight = "600",
				LineHeight = "1.5",
				LetterSpacing = "0.0075em"
			},
			H5 = new H5Typography()
			{
				FontFamily = new[] { "Pathway Gothic One" },
				FontSize = "1.25rem",
				FontWeight = "500",
				LineHeight = "1.6",
				LetterSpacing = "0.0075em"
			},
			H6 = new H6Typography()
			{
				FontFamily = new[] { "Pathway Gothic One" },
				FontSize = "1rem",
				FontWeight = "500",
				LineHeight = "1.6",
				LetterSpacing = "0.0075em"
			},
			Default = new DefaultTypography()
			{
				FontFamily = new[] { "Montserrat" }
			}
		},
		LayoutProperties = new LayoutProperties()
		{
			DefaultBorderRadius = "6px"
		},
		Shadows = new Shadow()
	};
}
