using BotAudioModule.Scripts;
using Discord.Commands;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class LoopQueue : MusicCommand
    {
        public LoopQueue(AudioService audioService) : base(audioService)
        {
        }

        [Alias("loop-queue")]
        [Command("loop", RunMode = RunMode.Async)]
        public async Task CommandAsync()
        {
            if (await CheckVoiceChannel(true) == false)
                return;

            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);

            if (AudioService.loopQueue)
                await ReplyAsync($"Disabled Queue Loop :x:");
            else
                await ReplyAsync($"Enabled Queue Loop :repeat:");

            AudioService.loopQueue = !AudioService.loopQueue;
        }
    }
}
