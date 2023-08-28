namespace Sparkle.Api.Shared.Helpers
{
    public static class ThrowHelper
    {
        public static SparkleException Throw<T>() => 
            new($"An exception occurred in {typeof(T).Name}");

        public static SparkleException Throw<T>(int? errorCode = null) => 
            new($"An exception occurred in {typeof(T)?.Name}", errorCode);

        public static SparkleException Throw<T>(string message, int? errorCode = null) => 
            new($"An exception occurred in {typeof(T)?.Name}: {message}", errorCode);
    }
}
