using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Victoria;
using Victoria.Entities;

namespace BotAudioModule.Scripts
{
    public static class EmbedManager
    {
        public static async Task SendVolumeChangedEmbed(this ISocketMessageChannel channel, int volume)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithColor(Color.Green)
                .WithTitle("Volume Changed")
                .WithDescription($"Volume changed to: {volume}");

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendErrorEmbed(this ISocketMessageChannel channel, string error)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithColor(Color.DarkRed)
                .WithTitle("An Error Occurred")
                .WithDescription(error);

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendAddedToQueueEmbed(this ISocketMessageChannel channel, LavaTrack track)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithColor(AudioModule.messageColor)
                .WithTitle("Song Added To Queue")
                .AddField(track.Author, $"[{track.Title}]({track.Uri})\n {track.Length.ToString(@"hh\:mm\:ss")}")
                .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(track.Uri));

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendNowPlayingEmbed(this ISocketMessageChannel channel, LavaTrack track)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithColor(AudioModule.messageColor)
                .WithTitle("Currently Playing")
                .AddField(track.Author, $"[{track.Title}]({track.Uri})\n" +
                    $"{track.Position.ToString(@"hh\:mm\:ss")} - {track.Length.ToString(@"hh\:mm\:ss")}")
                .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(track.Uri));

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendSongSkippedEmbed(this ISocketMessageChannel channel, LavaTrack track)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithColor(Color.LightOrange)
                .WithTitle("Song Skipped")
                .AddField(track.Author, $"[{track.Title}]({track.Uri})")
                .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(track.Uri));

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendCurrentQueueEmbed(this ISocketMessageChannel channel, LavaPlayer player, int songsPerPage = 15, int pages = 1, bool loopQueue = false, bool loopSong = false)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithTitle("Current Queue")
                .WithColor(AudioModule.messageColor)
                .WithDescription("**Currently Playing: **")
                .AddField(player.CurrentTrack.Author,
                $"[{player.CurrentTrack.Title}]({player.CurrentTrack.Uri}) - {player.CurrentTrack.Position.ToString(@"hh\:mm\:ss")}" +
                (loopSong ? "\n:repeat_one: Song Loop Enabled" : ""));

            if (player.Queue.Count > 0)
                builder.AddField("\u200b \n--------------------", "In the Queue :" +
                    (loopQueue ? "\n:repeat: Queue Loop Enabled" : ""));

            TimeSpan totalTime = TimeSpan.FromMilliseconds(0);
            int trackIndex = 1;
            foreach (var track in player.Queue.Items)
            {
                var t = (LavaTrack)track;

                if (trackIndex > (songsPerPage * pages) - songsPerPage && trackIndex <= (songsPerPage * pages))
                {
                    try
                    {
                        builder
                            .AddField($"**#{trackIndex}** *{t.Author}*",
                            $"[{t.Title}]({t.Uri}) - {t.Length.ToString(@"hh\:mm\:ss")}");
                    }
                    catch { }
                }
                totalTime += t.Length;
                trackIndex++;
            }

            totalTime += (player.CurrentTrack.Length - player.CurrentTrack.Position);
            builder.WithFooter($"{trackIndex} Songs - {totalTime.ToString(@"hh\:mm\:ss")} Listen Time");

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendForwardEmbed(this ISocketMessageChannel channel, LavaTrack track, int seconds)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithTitle("Playback Forward")
                .WithColor(AudioModule.messageColor)
                .AddField("Forward information",
                    $"{track.Position.ToString(@"hh\:mm\:ss")} => {track.Position + TimeSpan.FromSeconds(seconds)} [{track.Length.ToString(@"hh\:mm\:ss")}]")
                .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(track.Uri));

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendSeekEmbed(this ISocketMessageChannel channel, LavaTrack track, int seconds)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Playback Seek")
                .WithColor(AudioModule.messageColor)
                .WithCurrentTimestamp()
                .AddField("Seek information",
                    $"{track.Position.ToString(@"hh\:mm\:ss")} => {TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss")} [{track.Length.ToString(@"hh\:mm\:ss")}]")
                .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(track.Uri));

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendResumedPlaybackEmbed(this ISocketMessageChannel channel, LavaTrack track)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithTitle("Playback Resumed")
                .WithColor(AudioModule.messageColor)
                .AddField(track.Author, $"[{track.Title}]({track.Uri})")
                .AddField("Playback Progress", $"{track.Position.ToString(@"hh\:mm\:ss")} - {track.Length.ToString(@"hh\:mm\:ss")}")
                .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(track.Uri));

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendPausedPlaybackEmbed(this ISocketMessageChannel channel, LavaTrack track)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithTitle("Playback Paused")
                .WithColor(AudioModule.messageColor)
                .AddField(track.Author, $"[{track.Title}]({track.Uri})")
                .AddField("Playback Progress", $"{track.Position.ToString(@"hh\:mm\:ss")} - {track.Length.ToString(@"hh\:mm\:ss")}")
                .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(track.Uri));

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendRemovedFromQueueEmbed(this ISocketMessageChannel channel, LavaTrack track)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithTitle("Song Removed From Queue")
                .WithColor(AudioModule.messageColor)
                .AddField(track.Title, $"{track.Author}\n{track.Uri}")
                .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(track.Uri));

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendSongMovedEmbed(this ISocketMessageChannel channel)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithColor(AudioModule.messageColor)
                .WithTitle("Song Move")
                .WithDescription("Song has been successfully moved");

            await channel.SendMessageAsync("", false, builder.Build());
        }
    }
}
