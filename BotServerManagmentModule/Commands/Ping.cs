using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace VTCDiscordBot.Commands
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping", RunMode = RunMode.Async)]
        async Task CommandTask()
        {
            var t1 = DateTime.Now;
            var msg = await ReplyAsync("Pinging...");
            var t2 = DateTime.Now;

            var delta = t2 - t1;
            _ = msg.DeleteAsync();

            await ReplyAsync($"PONG 🏓\n" +
                $"Round Trip Time: {Context.Client.Latency}ms\n" +
                $"Gateway Delay: {(int)delta.TotalMilliseconds}ms");
        }
    }
}
