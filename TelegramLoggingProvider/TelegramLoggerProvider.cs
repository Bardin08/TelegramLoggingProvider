using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace TelegramLoggingProvider
{
    public class TelegramLoggerProvider : ILoggerProvider
    {
        private readonly TelegramLoggerProcessor _processor;
        private readonly TelegramLoggerOptions _options;
        private ConcurrentDictionary<string, TelegramLogger> _loggers = new();

        public TelegramLoggerProvider(TelegramLoggerProcessor processor, TelegramLoggerOptions options)
        {
            _processor = processor;
            _options = options;

        }

        public ILogger CreateLogger(string categoryName) => 
            _loggers.GetOrAdd(categoryName, name => new TelegramLogger(name, _options, _processor));

        public void Dispose() => _loggers.Clear();
    }
}
