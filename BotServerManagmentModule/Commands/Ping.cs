using Discord.Commands;
using System.Threading.Tasks;

namespace VTCDiscordBot.Commands
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        async Task CommandTask()
        {
            await ReplyAsync($"Ping : {Context.Client.Latency}ms");
        }
    }
}
