using BotAudioModule.Scripts;
using Discord.Commands;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class NowPlaying : MusicCommand
    {
        public NowPlaying(AudioService audioService) : base(audioService)
        {
        }

        [Alias("np", "currentsong", "songname", "song")]
        [Command("nowplaying")]
        async Task CommandTask()
        {
            if (await CheckVoiceChannel(true) == false)
                return;

            await Context.Channel.SendNowPlayingEmbed(
                AudioService.LavaClient.GetPlayer(Context.Guild.Id).CurrentTrack);
        }
    }
}
