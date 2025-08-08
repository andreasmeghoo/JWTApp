namespace JWTApp.Models.Logging
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string Method { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public long DurationMs { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
