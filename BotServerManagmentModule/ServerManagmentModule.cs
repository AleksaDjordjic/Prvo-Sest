using Discord.WebSocket;
using Discord;

namespace BotServerManagmentModule
{
    public class ServerManagmentModule
    { 
        public ServerLogger serverLogger;
        public static string botPrefix;
        public static Color messageColor;
        public static string customHelpText;
        public static string name;
        public static string announceImage;
        public static ulong logChannelID;

        public ServerManagmentModule(DiscordSocketClient socketClient, ulong _logChannelID, Color _messageColor, string _botPrefix, string _name, string _announceImage, string _customHelpText = "")
        {
            logChannelID = _logChannelID;
            botPrefix = _botPrefix;
            messageColor = _messageColor;
            customHelpText = _customHelpText;
            name = _name;
            announceImage = _announceImage;
            serverLogger = new ServerLogger(socketClient, logChannelID);
        }
    }
}
