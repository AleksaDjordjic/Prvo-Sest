using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class Leave : ModuleBase<SocketCommandContext>
    {
        [Alias("dc", "disconnect", "quit")]
        [Command("leave")]
        async Task CommandTask()
        {
            SocketGuildUser executionUser = (SocketGuildUser)Context.User;
            if(AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id).VoiceChannel == null)
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

                if (player.IsPlaying)
                    try { await player.StopAsync(); } catch { }

                AudioModule.audioService.loopCurrentSong = false;

                var channelName = player.VoiceChannel.Name;
                await AudioModule.audioService._lavalink.DefaultNode.DisconnectAsync(Context.Guild.Id);
                await ReplyAsync($"I've left `{channelName}` :wave:");
            }
            catch (Exception ex)
            {
                await ReplyAsync("Uh, oh ... ahem OOF ... an error occurred :\n```" + ex.ToString() + "```\n <@272472106380558336> ...?");
            }
        }
    }
}
