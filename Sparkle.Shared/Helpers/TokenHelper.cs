namespace Sparkle.Shared.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateToken() => Guid.NewGuid().ToString();

        public static string HashToken(string? token) =>
            string.IsNullOrWhiteSpace(token) ? string.Empty : BC.EnhancedHashPassword(token);

        public static bool VerifyToken(string? token, string? hash) =>
            !string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(hash) && BC.EnhancedVerify(token, hash);
    }
}
