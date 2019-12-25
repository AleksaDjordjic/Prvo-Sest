using System;

namespace DiscordBot.Scripts
{
    public static class Logger
    {
        public static void Log(string message, string sender, ConsoleColor consoleColor = ConsoleColor.White)
        {
            if (sender.Length > 12)
                sender.Substring(0, 12);

            Console.ForegroundColor = consoleColor;
            string _sender = sender;
            for (int i = 0; i < 12 - sender.Length; i++)
            {
                _sender += " ";
            }
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + _sender + message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
