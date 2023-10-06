namespace Sparkle.Shared
{
    public class SparkleException : Exception
    {
        public int? ErrorCode { get; set; }

        public SparkleException(string message, int? errorCode = null) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
