using Bindings;
using Discord;
using Discord.WebSocket;
using System.Collections.Generic;
using System.IO;

namespace BotHallMonitorModule
{
    public class HallMonitorModule
    {
        public static string botPrefix;
        public static Color messageColor;

        public static string HallMonitorMessageIDFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("BotHallMonitorModule.dll", "hall-monitor-message-id.txt");
        public static ulong HallMonitorMessageID = 0;

        public static ulong CurrentWeek = 1;

        public HallMonitorModule(DiscordSocketClient socketClient, Color _messageColor, string _botPrefix)
        {
            botPrefix = _botPrefix;
            messageColor = _messageColor;

            if (File.Exists(HallMonitorMessageIDFilePath))
                HallMonitorMessageID = ulong.Parse(File.ReadAllText(HallMonitorMessageIDFilePath));
        }
    }
}