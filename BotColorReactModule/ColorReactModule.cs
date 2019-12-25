using Discord;
using Discord.WebSocket;
using System.IO;

namespace BotColorReactModule
{
    public class ColorReactModule
    {
        public static string botPrefix;
        public static Color messageColor;

        public static string ColorReactionMessageIDFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("BotColorReactModule.dll", "color-reaction-message-id.txt");
        public static ulong ColorReactionMessageID = 0;

        public ColorReactModule(DiscordSocketClient socketClient, Color _messageColor, string _botPrefix)
        {
            botPrefix = _botPrefix;
            messageColor = _messageColor;

            socketClient.ReactionAdded += ReactionHandle.SocketClient_ReactionAdded;
            socketClient.ReactionRemoved += ReactionHandle.SocketClient_ReactionRemoved;

            if(File.Exists(ColorReactionMessageIDFilePath))
                ColorReactionMessageID = ulong.Parse(File.ReadAllText(ColorReactionMessageIDFilePath));
        }
    }
}