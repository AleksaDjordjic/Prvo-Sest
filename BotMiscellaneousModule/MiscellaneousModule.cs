using Discord;
using Discord.WebSocket;

namespace BotHallMonitorModule
{
    public class MiscellaneousModule
    {
        public static string botPrefix;
        public static Color messageColor;

        public MiscellaneousModule(DiscordSocketClient socketClient, Color _messageColor, string _botPrefix)
        {
            botPrefix = _botPrefix;
            messageColor = _messageColor;
        }
    }
}