using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace TelegramLoggingProvider
{
    internal class TelegramMessageFormatter
    {
        private readonly string _name;
        private readonly TelegramLoggerOptions _options;

        public TelegramMessageFormatter(string name, TelegramLoggerOptions options)
        {
            _name = name;
            _options = options;
        }

        public string Format<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
            {
                return string.Empty;
            }

            var level = _options.UseEmoji ? ToEmoji(logLevel) : ToString(logLevel);

            var sb = new StringBuilder();

            sb.Append('*').Append(level).Append(' ').AppendFormat("{0:hh:mm:ss}", DateTime.Now).Append("* ").Append(message);

            sb.AppendLine();
            sb.Append('`').Append(exception).Append('`');
            sb.AppendLine();

            sb.Append("_Initiator: ").Append(_name).Append('_');
            sb.AppendLine();
            sb.Append("_EventId: ").Append(eventId).Append('_');

            return sb.ToString();
        }

        private string ToString(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => "TRACE",
                LogLevel.Debug => "DEBUG",
                LogLevel.Information => "INFORMATION",
                LogLevel.Warning => "WARNING",
                LogLevel.Error => "ERROR",
                LogLevel.Critical => "CRITICAL",
                LogLevel.None => " ",
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, "")
            };
        }

        private string ToEmoji(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => "🔘",
                LogLevel.Debug => "⚪️",
                LogLevel.Information => "🟢",
                LogLevel.Warning => "‼️",
                LogLevel.Error => "🟠",
                LogLevel.Critical => "🔴",
                LogLevel.None => " ",
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, "")
            };
        }
    }
}