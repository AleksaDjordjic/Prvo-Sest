using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Bindings;

namespace DiscordBot
{
    public class Program
    {
        #region Variables
        DiscordSocketClient socketClient;
        CommandService commandService;
        IServiceProvider serviceProvider;

        string botToken = "NjU5MTQwMTA0NDAwNjY2NjI0.XgKA_g.iXEEfZ4mjVvJ86e3l_AQNQRWB58";
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

            ModuleManager.SetupModules(socketClient, serviceCollection, ref serviceProvider);

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
            await ModuleManager.RegisterModuleCommands(commandService, serviceProvider);
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