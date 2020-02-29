namespace Application.Extensions
{
	public static class StringExtensions
	{
		public static bool IsNullOrWhiteSpace(this string text)
		{
			return string.IsNullOrWhiteSpace(text);
		}

		public static bool HasContent(this string text)
		{
			return !string.IsNullOrWhiteSpace(text);
		}

	}
}
