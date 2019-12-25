using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Victoria.Entities;
using BotAudioModule;

namespace BotAudioModule.Commands
{
    public class NowPlaying : ModuleBase<SocketCommandContext>
    {
        [Alias("np", "currentsong", "songname", "song")]
        [Command("nowplaying")]
        async Task CommandTask()
        {
            var player = AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id);

            if(player.CurrentTrack == null)
                await ReplyAsync($"Music is not playing...");
            else
            {
                LavaTrack lp = player.CurrentTrack;

                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("Currently Playing")
                    .WithColor(AudioModule.messageColor)
                    .AddField(lp.Author, $"[{lp.Title}]({lp.Uri})\n" +
                    $"{lp.Position.ToString(@"hh\:mm\:ss")} - {lp.Length.ToString(@"hh\:mm\:ss")}")
                    .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(lp.Uri))
                    .WithCurrentTimestamp();

                await ReplyAsync("", false, builder.Build());
            }
        }
    }
}
