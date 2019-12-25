using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Victoria;
using Microsoft.Extensions.DependencyInjection;
using Discord;

namespace BotAudioModule
{
    public class AudioModule
    {
        DiscordSocketClient socketClient;
        public static string botPrefix;
        public static Color messageColor;

        IServiceProvider serviceProvider;
        private readonly LavaNode _lavaNode;

        public AudioModule(DiscordSocketClient _socketClient, IServiceCollection serviceCollection, Color _messageColor, string _botPrefix)
        {
            socketClient = _socketClient;
            messageColor = _messageColor;
            botPrefix = _botPrefix;

            serviceCollection
                .AddSingleton<LavaConfig>()
                .AddSingleton<LavaNode>();

            socketClient.Ready += SocketClient_Ready;

            _lavaNode = new LavaNode(socketClient, new LavaConfig() { });
        }

        public void FinalInit(IServiceProvider serviceProvider)
        {
            
        }

        public async Task SocketClient_Ready() => _lavaNode.ConnectAsync();
    }
}
