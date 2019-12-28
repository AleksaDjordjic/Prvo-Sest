using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using BotAudioModule.Scripts;

namespace BotAudioModule.Commands
{
    public class Skip : MusicCommand
    {
        public Skip(AudioService audioService) : base(audioService)
        {
        }

        [Alias("next", "nextsong", "s")]
        [Command("skip")]
        public async Task CommandTask()
        {
            if (await CheckVoiceChannel(true) == false)
                return;

            SocketGuildUser user = (SocketGuildUser)Context.User;
            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);

            var currentTrack = player.CurrentTrack;
            await Context.Channel.SendSongSkippedEmbed(currentTrack);

            if (player.Queue.Count >= 1)
                await player.SkipAsync();
            else await player.StopAsync();
        }
    }
}