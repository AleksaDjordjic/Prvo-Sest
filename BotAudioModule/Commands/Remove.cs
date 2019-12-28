using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;
using BotAudioModule.Scripts;
using Victoria.Entities;

namespace BotAudioModule.Commands
{
    public class Remove : MusicCommand
    {
        public Remove(AudioService audioService) : base(audioService)
        {
        }

        [Command("remove")]
        async Task CommandTask(int index)
        {
            if (await CheckVoiceChannel() == false)
                return;

            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);

            if (index > player.Queue.Count)
                await Context.Channel.SendErrorEmbed($"No song found with an index of {index}...");
            else
            {
                var song = player.Queue.Items.ToList()[index - 1];
                player.Queue.RemoveAt(index - 1);
                await Context.Channel.SendRemovedFromQueueEmbed((LavaTrack)song);
            }
        }
    }
}
