using Discord.Commands;
using System.Threading.Tasks;
using BotAudioModule.Scripts;

namespace BotAudioModule.Commands
{
    public class Resume : MusicCommand
    {
        public Resume(AudioService audioService) : base(audioService)
        {
        }

        [Command("resume")]
        async Task CommandTask()
        {
            if (await CheckVoiceChannel(true) == false)
                return;

            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);
            if (player.IsPaused)
            {
                await Context.Channel.SendResumedPlaybackEmbed(player.CurrentTrack);
                await player.ResumeAsync();
            }
            else
                await Context.Channel.SendErrorEmbed("Playback is already playing...");
        }
    }
}
