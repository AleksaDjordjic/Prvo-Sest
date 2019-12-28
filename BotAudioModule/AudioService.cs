using BotAudioModule.Scripts;
using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using Victoria;
using Victoria.Entities;

namespace BotAudioModule
{
    public sealed class AudioService
    {
        public static LavaRestClient LavaRestClient;
        public static LavaSocketClient LavaClient;
        public static DiscordSocketClient discordClient;

        public AudioService(LavaRestClient lavaRestClient, DiscordSocketClient client,
            LavaSocketClient lavaSocketClient)
        {
            discordClient = client;
            LavaRestClient = lavaRestClient;
            LavaClient = lavaSocketClient;
        }

        public Task InitializeAsync()
        {
            discordClient.Ready += ClientReadyAsync;
            LavaClient.OnTrackFinished += TrackFinished;
            return Task.CompletedTask;
        }

        private async Task ClientReadyAsync()
        {
            await LavaClient.StartAsync(discordClient);
        }

        private async Task TrackFinished(LavaPlayer player, LavaTrack track, TrackEndReason reason)
        {
            if (!reason.ShouldPlayNext())
                return;

            if (!player.Queue.TryDequeue(out var item) || !(item is LavaTrack nextTrack))
            {
                await player.TextChannel.SendMessageAsync("There are no more tracks in the queue.");
                return;
            }

            await player.PlayAsync(nextTrack);
            await ((ISocketMessageChannel)player.TextChannel).SendNowPlayingEmbed(nextTrack);
        }

        public static string GetYoutubeVideoThumbnailLink(Uri URI)
        {
            string videoID = URI.ToString().Replace("https://www.youtube.com/watch?v=", "");
            return $"https://img.youtube.com/vi/{videoID}/mqdefault.jpg";
        }
    }
}