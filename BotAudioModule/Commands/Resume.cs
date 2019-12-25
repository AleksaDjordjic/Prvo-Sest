using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

using BotAudioModule;

namespace BotAudioModule.Commands
{
    public class Resume : ModuleBase<SocketCommandContext>
    {
        [Command("resume")]
        async Task CommandTask()
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
                if (player.IsPaused)
                {
                    await player.PauseAsync();
                    EmbedBuilder builder = new EmbedBuilder();
                    builder.WithTitle("Playback Resumed")
                        .WithColor(AudioModule.messageColor)
                        .AddField(player.CurrentTrack.Author, $"[{player.CurrentTrack.Title}]({player.CurrentTrack.Uri})")
                        .AddField("Playback Progress", $"{player.CurrentTrack.Position.ToString(@"hh\:mm\:ss")} - {player.CurrentTrack.Length.ToString(@"hh\:mm\:ss")}")
                        .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(player.CurrentTrack.Uri))
                        .WithCurrentTimestamp();
                    await ReplyAsync("", false, builder.Build());
                }
                else
                {
                    await ReplyAsync("Playback is already playing...");
                }
            }
            catch (Exception ex)
            {
                await ReplyAsync("Uh, oh ... ahem OOF ... an error occurred :\n```" + ex.ToString() + "```\n <@272472106380558336> ...?");
            }
        }
    }
}
