using System.Threading.Tasks;

namespace TelegramLoggingProvider.Interfaces
{
    public interface ITelegramWriter
    {
        /// <summary>
        /// Sends the message to a telegram.
        /// </summary>
        /// <param name="message"> String representation of the message which should be sent. </param>
        /// <returns> Message sending result. </returns>
        Task<bool> WriteAsync(string message);
    }
}
