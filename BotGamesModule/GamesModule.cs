using Discord;
using Discord.WebSocket;

namespace BotGamesModule
{
    public class GamesModule
    {
        public static string botPrefix;
        public static Color messageColor;

        public GamesModule(DiscordSocketClient socketClient, Color _messageColor, string _botPrefix)
        {
            botPrefix = _botPrefix;
            messageColor = _messageColor;
        }
    }
}
