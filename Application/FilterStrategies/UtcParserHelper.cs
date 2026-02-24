namespace Application.FilterStrategies
{
	internal class UtcParserHelper
	{
		public static bool TryParseUtcDate(string? input, out DateTime utcDate)
		{
			utcDate = default;
			if (string.IsNullOrWhiteSpace(input)) return false;
			if (!DateTime.TryParse(input.Trim(), out var dt)) return false;
			utcDate = dt.Kind == DateTimeKind.Utc ? dt : dt.ToUniversalTime();
			return true;
		}
	}
}
