using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class Join : ModuleBase<SocketCommandContext>
    {
        [Alias("summon")]
        [Command("join")]
        public async Task CommandTask()
        {
            SocketGuildUser user = (SocketGuildUser)Context.User;

            if (user.VoiceChannel == null)
                await ReplyAsync(":x: You Must First Join a Voice Channel");
            else
            {
                await AudioModule.audioService._lavalink.DefaultNode.ConnectAsync(user.VoiceChannel, Context.Message.Channel);
                AudioModule.audioService.Options.TryAdd(user.Guild.Id, new AudioOptions
                {
                    Summoner = user
                });
                await ReplyAsync($":white_check_mark: Connected to `{user.VoiceChannel.Name}` and bound to `{Context.Message.Channel.Name}`");
            }          
        }
    }
}
