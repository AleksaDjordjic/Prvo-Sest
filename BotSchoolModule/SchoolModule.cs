using Discord;
using Discord.WebSocket;

namespace BotSchoolModule
{
    public class SchoolModule
    {
        public static string botPrefix;
        public static Color messageColor;

        public SchoolModule(DiscordSocketClient socketClient, Color _messageColor, string _botPrefix)
        {
            botPrefix = _botPrefix;
            messageColor = _messageColor;
        }
    }
}