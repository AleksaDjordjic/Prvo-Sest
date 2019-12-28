using BotAudioModule.Scripts;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class Volume : MusicCommand
    {
        public Volume(AudioService audioService) : base(audioService)
        {
        }

        [Command("volume", RunMode = RunMode.Async)]
        async Task CommandTask(int volume)
        {
            if (await CheckVoiceChannel() == false)
                return;
            
            SocketGuildUser user = (SocketGuildUser)Context.User;
            if (volume > 150 || volume < 5)
            {
                await Context.Channel.SendErrorEmbed("Volume must be between 150 and 5!");
                return;
            }

            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);
            await player.SetVolumeAsync(volume);
            await Context.Channel.SendVolumeChangedEmbed(volume);
        }
    }
}
