using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace BotServerManagmentModule.Commands
{
    public class StaffHelp : ModuleBase<SocketCommandContext>
    {
        [Command("help-admin"), RequireUserPermission(ChannelPermission.ManageChannels)]
        public async Task CommandAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            string customHelpText = ServerManagmentModule.customHelpText == "" ? "" : $"\n{ServerManagmentModule.customHelpText}";

            builder.WithTitle("Available Server Managment Commands")
                .WithColor(ServerManagmentModule.messageColor)
                .WithDescription($"**Bot Prefix :** `{ServerManagmentModule.botPrefix}`\n" +
                $"**ping** - Shows current Bot ping to Discord\n" +
                $"**say <channel> <text>** - Send the specified text to the mentioned channel\n" +
                $"**announce <channel> \"<title>\" <message>** - Send an Embed announcment to the specified text channel\n" +
                $"**purge <amount>** - Purged the set amount of messages\n" + customHelpText)
                .WithCurrentTimestamp();

            await ReplyAsync("", false, builder.Build());
        }
    }
}
