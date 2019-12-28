using Discord.WebSocket;
using System;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Victoria;

namespace BotAudioModule
{
    public class AudioModule
    {
        DiscordSocketClient socketClient;
        public static string botPrefix;
        public static Color messageColor;

        IServiceProvider serviceProvider;

        public AudioModule(DiscordSocketClient _socketClient, IServiceCollection serviceCollection, Color _messageColor, string _botPrefix)
        {
            socketClient = _socketClient;
            messageColor = _messageColor;
            botPrefix = _botPrefix;

            serviceCollection
                .AddSingleton<LavaRestClient>()
                .AddSingleton<LavaSocketClient>()
                .AddSingleton<AudioService>();
        }

        public void FinalInit(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<AudioService>().InitializeAsync();
        }
    }
}
