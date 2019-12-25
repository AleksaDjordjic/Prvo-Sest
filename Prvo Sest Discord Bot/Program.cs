using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Discord;
using BotAudioModule;
using DiscordBot.Scripts;
using BotServerManagmentModule;
using BotColorReactModule;

namespace DiscordBot
{
    public class Program
    {
        #region Variables
        DiscordSocketClient socketClient;
        CommandService commandService;
        IServiceProvider serviceProvider;

        AudioModule audioModule;
        ServerManagmentModule managmentModule;
        ColorReactModule colorReactModule;

        string botToken = "NjU5MTQwMTA0NDAwNjY2NjI0.XgKA_g.iXEEfZ4mjVvJ86e3l_AQNQRWB58";
        #endregion

        static void Main(string[] args)
        {
            new Program().RunBot().GetAwaiter().GetResult();
        }

        #region Tasks
        public async Task RunBot()
        {
            DiscordSocketConfig config = new DiscordSocketConfig
            { MessageCacheSize = 100, AlwaysDownloadUsers = true };

            socketClient = new DiscordSocketClient(config);
            commandService = new CommandService();

            var serviceCollection = new ServiceCollection()
                .AddSingleton(socketClient)
                .AddSingleton(commandService);

            managmentModule = new ServerManagmentModule(socketClient, 659161580013092875, Static.Color, Static.Prefix, "1/6", "");
            colorReactModule = new ColorReactModule(socketClient, Static.Color, Static.Prefix);

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
            await socketClient.SetGameAsync($"{Static.Prefix}help / {Static.Prefix}pomoc", null, ActivityType.Listening);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommanedsAsync()
        {
            socketClient.MessageReceived += HandleMessageRecieved;
            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
            await commandService.AddModulesAsync(typeof(AudioModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(ServerManagmentModule).Assembly, serviceProvider);
            await commandService.AddModulesAsync(typeof(ColorReactModule).Assembly, serviceProvider);
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