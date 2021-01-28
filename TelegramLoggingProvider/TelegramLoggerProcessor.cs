using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace TelegramLoggingProvider
{
    public class TelegramLoggerProcessor : IDisposable
    {
        private const int MaxEnqueuedMessages = 1024;
        private const int Timeout = 1000;

        private readonly BlockingCollection<string> _queue = new(MaxEnqueuedMessages);
        private readonly TelegramWriter _writer;
        private readonly Thread _thread;

        public TelegramLoggerProcessor(TelegramLoggerOptions options)
        {
            _writer = new TelegramWriter(options.AccessToken, options.ChatId);

            _thread = new Thread(async () => await ProcessLogQueue())
            {
                IsBackground = true,
                Name = "Telegram logger queue process thread"
            };

            _thread.Start();
        }

        public void EnqueueMessage(string message)
        {
            if (!_queue.IsAddingCompleted)
            {
                try
                {
                    _queue.Add(message);
                }
                catch
                {
                }
            }
        }

        private async Task ProcessLogQueue()
        {
            try
            {
                foreach (var msg in _queue.GetConsumingEnumerable())
                {
                    var messageSent = await _writer.WriteAsync(msg);

                    if (!messageSent)
                    {
                        EnqueueMessage(msg);
                    }
                }
            }
            catch
            {
                try
                {
                    _queue.CompleteAdding();
                }
                catch
                {
                }
            }
        }

        public void Dispose()
        {
            try
            {
                _queue.CompleteAdding();
                _thread.Join(Timeout);
            }
            catch
            {
            }
        }
    }
}