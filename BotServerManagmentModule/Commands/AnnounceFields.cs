using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BotServerManagmentModule.Commands
{
    public class AnnounceFields : ModuleBase<SocketCommandContext>
    {
        [Command("announce-fields", RunMode = RunMode.Async), RequireUserPermission(ChannelPermission.ManageChannels)]
        async Task CommandTask(ISocketMessageChannel channel, string title, string description, [Remainder]string fieldData)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(title)
                .WithColor(ServerManagmentModule.messageColor)
                .WithDescription(description)
                .WithFooter(ServerManagmentModule.name, ServerManagmentModule.announceImage)
                .WithCurrentTimestamp();

            foreach (var str in fieldData.Split("&;"))
            {
                var v = str.Split("$;", 2);
                builder.AddField(v[0], v[1]);
            }

            await channel.SendMessageAsync("", false, builder.Build());
        }
    }
}