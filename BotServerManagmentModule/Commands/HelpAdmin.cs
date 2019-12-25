using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace BotServerManagmentModule.Commands
{
    public class HelpAdmin : ModuleBase<SocketCommandContext>
    {
        [Command("help-admin"), RequireUserPermission(ChannelPermission.ManageChannels)]
        public async Task CommandAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            string customHelpText = ServerManagmentModule.customHelpText == "" ? "" : $"\n{ServerManagmentModule.customHelpText}";

            var prefix = ServerManagmentModule.botPrefix;

            builder.WithTitle($"Komande za Admine - Bot Prefix: {prefix}")
                .WithColor(ServerManagmentModule.messageColor)
                .WithDescription(
                $"`{prefix}ping` - Salje RTT i Gateway Delay Bota\n" +
                $"`{prefix}say <#channel> <text>` - Posalje <text> u odredjen <#channel>\n" +
                $"`{prefix}announce <channel> \"<title>\" <message>` - Posalje Embed sa naslovom <title>, desckripcijom <message> u odredjen <#channel>\n" +
                $"`{prefix}purge <amount>` - Obrise poslednjih <amount> poruka u trenutnom kanalu\n" + customHelpText)
                .WithCurrentTimestamp();

            await ReplyAsync("", false, builder.Build());
        }
    }
}
