using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class Volume : ModuleBase<SocketCommandContext>
    {
        [Command("volume")]
        async Task CommandTask(int volume)
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

            if (volume > 150 || volume < 0)
            {
                await ReplyAsync($"Volume must be between 0 and 150");
                return;
            }
            try
            {
                var player = AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id);
                await player.SetVolumeAsync(volume);
                await ReplyAsync($"Volume has been set to {volume}");
            }
            catch (InvalidOperationException ex)
            {
                await ReplyAsync("Uh, oh ... ahem OOF ... an error occurred :\n```" + ex.ToString() + "```\n <@272472106380558336> ...?");
            }
        }
    }
}
