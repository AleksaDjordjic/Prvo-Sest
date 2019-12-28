using Discord.Commands;
using System.Threading.Tasks;
using BotAudioModule.Scripts;

namespace BotAudioModule.Commands
{
    public class Pause : MusicCommand
    {
        public Pause(AudioService audioService) : base(audioService)
        {
        }

        [Command("pause", RunMode = RunMode.Async)]
        async Task CommandTask()
        {
            if (await CheckVoiceChannel(true) == false)
                return;

            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);
            if (player.IsPaused)
                await Context.Channel.SendErrorEmbed("Playback is already paused...");
            else
            {
                await Context.Channel.SendPausedPlaybackEmbed(player.CurrentTrack);
                await player.PauseAsync();
            }            
        }
    }
}
