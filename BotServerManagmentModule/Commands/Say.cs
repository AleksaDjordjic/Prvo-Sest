using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BotServerManagmentModule
{
    public class Say : ModuleBase<SocketCommandContext>
    {
        [Command("say"), RequireUserPermission(ChannelPermission.ManageMessages)]
        async Task CommandTask(ISocketMessageChannel channel, [Remainder]string text = "")
        {
            await channel.SendMessageAsync(text);
        }
    }
}
