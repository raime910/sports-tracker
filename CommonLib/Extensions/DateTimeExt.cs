using System;

namespace CommonLib.Extensions
{
	public static class DateTimeExt 
	{
		/// <summary>
		/// Returns age of a date time instance.
		/// </summary>
		/// <param name="dateTime">DateTime instance.</param>
		public static int GetAge(this DateTime dateTime)
		{
			var now = DateTime.Today;
			var age = now.Year - dateTime.Year;

			if (now < dateTime.AddYears(age))
			{
				age--;
			}

			return age;
		}

		/// <summary>
		/// Returns a (standard) formatted date time string.
		/// </summary>
		/// <param name="dateTime">DateTime isntance.</param>
		/// <returns></returns>
		public static string ToStdDateString(this DateTime dateTime)
		{
			return dateTime.ToString("MM/dd/yyyy");
		}
	}
}
