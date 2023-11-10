namespace Sparkle.Shared.Extensions
{
    public static class StringExtension
    {
        public static int? GetNumber(this string? str)
        {
            return str == null ? null : int.Parse(string.Concat(str.Where(char.IsDigit)));
        }
    }
}
