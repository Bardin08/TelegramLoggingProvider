using Microsoft.Extensions.Logging;
using System;

namespace TelegramLoggingProvider
{
    public class TelegramLogger : ILogger
    {
        private readonly TelegramLoggerProcessor _loggingProcessor;
        private readonly TelegramMessageFormatter _formatter;
        private readonly TelegramLoggerOptions _options;

        public TelegramLogger(string name, TelegramLoggerOptions options, TelegramLoggerProcessor loggingProcessor)
        {
            _loggingProcessor = loggingProcessor ?? throw new ArgumentNullException(nameof(loggingProcessor));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _formatter = new TelegramMessageFormatter(name, options);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = _formatter.Format(logLevel, eventId, state, exception, formatter);

            if (!string.IsNullOrEmpty(message))
            {
                _loggingProcessor.EnqueueMessage(message);
            }
        }

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) => _options.LogLevel >= logLevel;
    }
}
