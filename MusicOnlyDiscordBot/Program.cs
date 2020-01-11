using Bindings;
using BotAudioModule;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MusicOnlyDiscordBot
{
    public class Program
    {
        #region Variables
        DiscordSocketClient socketClient;
        CommandService commandService;
        IServiceProvider serviceProvider;
        AudioModule audioModule;

        string botToken = "NjY1NTg2NDQ1NDY0ODk1NTA3.XhnzEw.s29-NG20Eg9P-yBcOQ8n1MKE-AI";
        #endregion

        static void Main(string[] args)
        {
            new Program().RunBot().GetAwaiter().GetResult();
        }

        #region Tasks
        public async Task RunBot()
        {
            DiscordSocketConfig config = new DiscordSocketConfig { MessageCacheSize = 100, AlwaysDownloadUsers = true };

            socketClient = new DiscordSocketClient(config);
            commandService = new CommandService();

            var serviceCollection = new ServiceCollection()
                .AddSingleton(socketClient)
                .AddSingleton(commandService);

            audioModule = new AudioModule(socketClient, serviceCollection, Static.Color, Static.Prefix);
            serviceProvider = serviceCollection.BuildServiceProvider();
            audioModule.FinalInit(serviceProvider);

            socketClient.Log += Log;

            await UpdateGameActivity();
            await socketClient.SetStatusAsync(UserStatus.Online);
            await RegisterCommanedsAsync();
            await socketClient.LoginAsync(TokenType.Bot, botToken);
            await socketClient.StartAsync();

            await Task.Delay(-1);
        }

        public async Task UpdateGameActivity()
        {
            await socketClient.SetGameAsync($"{Static.Prefix}help-music", null, ActivityType.Listening);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommanedsAsync()
        {
            socketClient.MessageReceived += HandleMessageRecieved;
            await commandService.AddModulesAsync(typeof(AudioModule).Assembly, serviceProvider);
        }

        private async Task HandleMessageRecieved(SocketMessage arg)
        {
            SocketUserMessage message = arg as SocketUserMessage;
            if (message == null || message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix(Static.Prefix, ref argPos) || message.HasMentionPrefix(socketClient.CurrentUser, ref argPos))
            {
                SocketCommandContext context = new SocketCommandContext(socketClient, message);

                IResult resoult = await commandService.ExecuteAsync(context, argPos, serviceProvider);

                if (!resoult.IsSuccess)
                    Logger.Log(resoult.ErrorReason, "Error", ConsoleColor.Red);
            }
        }
        #endregion
    }
}
