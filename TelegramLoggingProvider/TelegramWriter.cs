using System.Threading.Tasks;

using Telegram.Bot;
using TelegramLoggingProvider.Interfaces;

namespace TelegramLoggingProvider
{
    class TelegramWriter : ITelegramWriter
    {
        private readonly ITelegramBotClient _botClient;
        private readonly string _chatId;

        public TelegramWriter(string accessToken, string chatId)
        {
            _botClient = new TelegramBotClient(accessToken);
            _chatId = chatId;
        }

        public async Task<bool> WriteAsync(string message)
        {
            var sentMessage = await _botClient.SendTextMessageAsync(_chatId, message, Telegram.Bot.Types.Enums.ParseMode.Markdown);

            return sentMessage != null;
        }
    }
}
