using Discord.Commands;
using System;
using System.Threading.Tasks;
using BotAudioModule.Scripts;

namespace BotAudioModule.Commands
{
    public class Seek : MusicCommand
    {
        public Seek(AudioService audioService) : base(audioService)
        {
        }

        [Command("seek", RunMode = RunMode.Async)]
        async Task CommandTask(int seconds)
        {
            if (await CheckVoiceChannel(true) == false)
                return;

            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);

            if(seconds < 0)
                await Context.Channel.SendErrorEmbed("Cannot seek to negative numbers...");
            else if (TimeSpan.FromSeconds(seconds) >= player.CurrentTrack.Length)
                await Context.Channel.SendErrorEmbed("Cannot seek further than the songs length, for that use `skip`");
            else
            {
                await Context.Channel.SendSeekEmbed(player.CurrentTrack, seconds);
                await player.SeekAsync(TimeSpan.FromSeconds(seconds));
            }
        }
    }
}
