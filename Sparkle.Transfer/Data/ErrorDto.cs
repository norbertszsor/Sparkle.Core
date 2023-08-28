namespace Sparkle.Transfer.Data
{
    public class ErrorDto
    {
        public int? ErrorCode { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
