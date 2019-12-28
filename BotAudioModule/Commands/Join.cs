using BotAudioModule.Scripts;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class Join : MusicCommand
    {
        public Join(AudioService audioService) : base(audioService)
        {
        }

        [Alias("summon")]
        [Command("join", RunMode = RunMode.Async)]
        public async Task CommandTask()
        {
            SocketGuildUser user = (SocketGuildUser)Context.User;

            if (user.VoiceChannel == null)
                await ReplyAsync(":x: You Must First Join a Voice Channel");
            else if(AudioService.LavaClient.GetPlayer(Context.Guild.Id) != null)
            {
                await ReplyAsync($"Im already in a Voice Channel...");
            }
            else
            {
                await AudioService.LavaClient.ConnectAsync(user.VoiceChannel, (ITextChannel)Context.Channel);
                await ReplyAsync($":white_check_mark: Connected to `{user.VoiceChannel.Name}` and bound to `{Context.Message.Channel.Name}`");
            }          
        }
    }
}
