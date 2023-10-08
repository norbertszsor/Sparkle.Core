namespace Sparkle.Shared.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateToken() => Guid.NewGuid().ToString();

        public static string HashToken(string? token) => BC.EnhancedHashPassword(token);

        public static bool VerifyToken(string? token, string? hash) => BC.EnhancedVerify(token, hash);
    }
}
