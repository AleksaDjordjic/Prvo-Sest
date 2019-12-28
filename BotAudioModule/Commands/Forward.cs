using BotAudioModule.Scripts;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class Forward : MusicCommand
    {
        public Forward(AudioService audioService) : base(audioService)
        {
        }

        [Alias("fwd", "fw")]
        [Command("forward", RunMode = RunMode.Async)]
        async Task CommandTask(int seconds)
        {
            if (await CheckVoiceChannel(true) == false)
                return;

            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);

            if (seconds < player.CurrentTrack.Position.Seconds)
                await Context.Channel.SendErrorEmbed("Can't forward before the song...");
            else if (TimeSpan.FromSeconds(seconds) >= player.CurrentTrack.Length - player.CurrentTrack.Position - TimeSpan.FromSeconds(5))
                await Context.Channel.SendErrorEmbed("Can't forward further than the songs length, for that use `skip`");
            else
            {
                await Context.Channel.SendForwardEmbed(player.CurrentTrack, seconds);
                await player.SeekAsync(player.CurrentTrack.Position + TimeSpan.FromSeconds(seconds));
            }
        }
    }
}
