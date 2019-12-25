using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

using BotAudioModule;

namespace BotAudioModule.Commands
{
    public class Skip : ModuleBase<SocketCommandContext>
    {
        [Alias("next", "nextsong")]
        [Command("skip")]
        public async Task CommandTask()
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

            try
            {
                var player = AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id);
                if (player == null)
                {
                    await ReplyAsync($"Music is not playing...");
                    return;
                }
                
                try
                {
                    var currentTrack = player.CurrentTrack;

                    if(currentTrack == null)
                    {
                        await ReplyAsync($"Music is not playing...");
                        return;
                    }

                    EmbedBuilder builder = new EmbedBuilder();
                    builder
                        .WithTitle("Song Skipped")
                        .WithColor(AudioModule.messageColor)
                        .AddField(currentTrack.Author, $"[{currentTrack.Title}]({currentTrack.Uri})")
                        .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(currentTrack.Uri))
                        .WithCurrentTimestamp();

                    AudioModule.audioService.loopCurrentSong = false;

                    await ReplyAsync("", false, builder.Build());

                    if (player.IsPlaying)
                        await player.StopAsync();
                }
                catch (Exception ex)
                {
                    await ReplyAsync("Uh, oh ... ahem OOF ... an error occurred :\n```" + ex.ToString() + "```\n <@272472106380558336> ...?");
                }
            }
            catch (Exception ex)
            {
                await ReplyAsync("Uh, oh ... ahem OOF ... an error occurred :\n```" + ex.ToString() + "```\n <@272472106380558336> ...?");
            }
        }
    }
}
