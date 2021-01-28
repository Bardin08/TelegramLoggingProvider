using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace TelegramLoggingProvider
{
    public static class TelegramLoggerExtensions
    {
        public static ILoggingBuilder AddTelegram(this ILoggingBuilder builder)
        {
            return builder.AddTelegram(new TelegramLoggerOptions());
        }

        public static ILoggingBuilder AddTelegram(this ILoggingBuilder builder, Action<TelegramLoggerOptions> options)
        {
            var config = new TelegramLoggerOptions();
            options(config);

            return builder.AddTelegram(config);
        }

        public static ILoggingBuilder AddTelegram(this ILoggingBuilder builder, IConfiguration configuration)
        {
            var options = new TelegramLoggerOptions();

            configuration.GetSection("Logging:Telegram")?.Bind(options);

            return builder.AddTelegram(options);
        }

        public static ILoggingBuilder AddTelegram(this ILoggingBuilder builder, TelegramLoggerOptions options)
        {
            var loggerProcessor = new TelegramLoggerProcessor(options);

            return builder.AddProvider(new TelegramLoggerProvider(loggerProcessor, options));
        }

    }
}
