using BotAudioModule.Scripts;
using Discord.Commands;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class Queue : MusicCommand
    {
        public Queue(AudioService audioService) : base(audioService)
        {
        }

        [Command("queue", RunMode = RunMode.Async)]
        async Task CommandTask([Remainder] int pages = 1)
        {
            if (await CheckVoiceChannel(true) == false)
                return;

            await Context.Channel.SendCurrentQueueEmbed(
                AudioService.LavaClient.GetPlayer(Context.Guild.Id), 
                15, pages);
        }
    }
}