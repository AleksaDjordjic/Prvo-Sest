using Discord;
using Discord.WebSocket;

namespace BotFunModule
{
    public class FunModule
    {
        public static string botPrefix;
        public static Color messageColor;

        public FunModule(DiscordSocketClient socketClient, Color _messageColor, string _botPrefix)
        {
            botPrefix = _botPrefix;
            messageColor = _messageColor;
        }
    }
}