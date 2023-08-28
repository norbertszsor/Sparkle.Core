namespace Sparkle.Api.Shared.Extensions
{
    public static class StringExstension
    {
        public static int? GetNumber(this string? str)
        {
            if (str == null)
            {
                return null;
            }

            return int.Parse(string.Concat(str.Where(char.IsDigit)));
        }
    }
}
