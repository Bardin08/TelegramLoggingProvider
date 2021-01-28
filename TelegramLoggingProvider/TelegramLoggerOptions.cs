using Microsoft.Extensions.Logging;

namespace TelegramLoggingProvider
{
    public class TelegramLoggerOptions
    {
        public LogLevel LogLevel { get; set; }
        public string AccessToken { get; set; }
        public string ChatId { get; set; }
        public bool UseEmoji { get; set; }
    }
}