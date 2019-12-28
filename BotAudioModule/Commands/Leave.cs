using BotAudioModule.Scripts;
using Discord.Commands;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class Leave : MusicCommand
    {
        public Leave(AudioService audioService) : base(audioService)
        {
        }

        [Alias("dc", "disconnect", "quit")]
        [Command("leave", RunMode = RunMode.Async)]
        async Task CommandTask()
        {
            if (await CheckVoiceChannel(true) == false)
                return;

            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);

            if (player.IsPlaying)
                try { await player.StopAsync(); } catch { }

            await AudioService.LavaClient.DisconnectAsync(player.VoiceChannel);
            await ReplyAsync($"I've left `{player.VoiceChannel.Name}` :wave:");
        }
    }
}
