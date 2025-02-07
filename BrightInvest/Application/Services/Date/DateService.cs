namespace BrightInvest.Application.Services.Date
{
	public class DateService
	{
		public static DateTime GetLastWorkingDay(DateTime today)
		{
			return today.DayOfWeek switch
			{
				DayOfWeek.Monday => today.AddDays(-3),
				DayOfWeek.Sunday => today.AddDays(-2),
				DayOfWeek.Saturday => today.AddDays(-1),
				_ => today.AddDays(-1)
			};
		}
	}
}
