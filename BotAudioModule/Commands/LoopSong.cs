using BotAudioModule.Scripts;
using Discord.Commands;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class LoopSong : MusicCommand
    {
        public LoopSong(AudioService audioService) : base(audioService)
        {
        }

        [Command("loop-song")]
        async Task CommandTask()
        {
            if (await CheckVoiceChannel(true) == false)
                return;

            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);

            if(AudioService.loopSong)
                await ReplyAsync($"Disabled loop for `{player.CurrentTrack.Title}` :x:");
            else
                await ReplyAsync($"Enabled loop for `{player.CurrentTrack.Title}` :repeat_one:");

            AudioService.loopSong = !AudioService.loopSong;
        }
    }
}
