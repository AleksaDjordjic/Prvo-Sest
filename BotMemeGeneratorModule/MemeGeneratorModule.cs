using Discord;
using Discord.WebSocket;

namespace BotMemeGeneratorModule
{
    public class MemeGeneratorModule    
    {
        public static string botPrefix;
        public static Color messageColor;

        public MemeGeneratorModule(DiscordSocketClient socketClient, Color _messageColor, string _botPrefix)
        {
            botPrefix = _botPrefix;
            messageColor = _messageColor;
        }
    }
}
