using BotServerManagmentModule;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BotServerManagmentModule.Commands
{
    public class Announce : ModuleBase<SocketCommandContext>
    {
        [Command("announce"), RequireUserPermission(GuildPermission.Administrator)]
        async Task CommandTask(ISocketMessageChannel channel, string title, [Remainder]string message)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(title)
                .WithColor(ServerManagmentModule.messageColor)
                .WithDescription(message)
                .WithFooter(BotServerManagmentModule.ServerManagmentModule.name, BotServerManagmentModule.ServerManagmentModule.announceImage)
                .WithCurrentTimestamp();

            await channel.SendMessageAsync("", false, builder.Build());
        }
    }
}
