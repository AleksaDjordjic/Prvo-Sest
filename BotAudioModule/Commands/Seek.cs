using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

using BotAudioModule;

namespace BotAudioModule.Commands
{
    public class Seek : ModuleBase<SocketCommandContext>
    {
        [Command("seek")]
        async Task CommandTask(int seconds)
        {
            SocketGuildUser executionUser = (SocketGuildUser)Context.User;
            if (AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id).VoiceChannel == null)
            {
                await ReplyAsync("Bot isn't connected to a channel at the moment...");
                return;
            }

            if (executionUser.VoiceChannel == null || executionUser.VoiceChannel != AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id).VoiceChannel)
            {
                await ReplyAsync("You cannot perform this command while not in the voice channel!");
                return;
            }

            var player = AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id);

            if(player.CurrentTrack == null)
                await ReplyAsync($"Music is not playing...");
            else
            {
                if(seconds < 0)
                    await ReplyAsync("Cannot seek to negative numbers...");
                else if (TimeSpan.FromSeconds(seconds) >= player.CurrentTrack.Length)
                    await ReplyAsync("Cannot seek further than the songs length, for that use `skip`");
                else
                {
                    EmbedBuilder builder = new EmbedBuilder();
                    builder.WithTitle("Playback Seek")
                        .WithColor(AudioModule.messageColor)
                        .WithCurrentTimestamp()
                        .AddField("Seek information", 
                        $"{player.CurrentTrack.Position.ToString(@"hh\:mm\:ss")} => {TimeSpan.FromSeconds(seconds)} [{player.CurrentTrack.Length.ToString(@"hh\:mm\:ss")}]")
                        .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(player.CurrentTrack.Uri));
                    await player.SeekAsync(TimeSpan.FromSeconds(seconds));
                    await ReplyAsync("", false, builder.Build());
                }
            }
        }
    }
}
