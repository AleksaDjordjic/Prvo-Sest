using Discord;
using Discord.Commands;
using System.IO;
using System.Threading.Tasks;

namespace BotHallMonitorModule.Commands
{
    public class PostInitialMessage : ModuleBase<SocketCommandContext>
    {

        [Command("hall-monitor-message")]
        public async Task CommandAsync()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("Redari")
                .WithColor(HallMonitorModule.messageColor)
                .WithDescription("Ovde mozete da vidite:\n" +
                "Ko bio redar\n" +
                "Ko je sada redar\n" +
                "Ko ce biti redar\n" +
                "3 u 1 <:emoji_9:659196392035713065>")
                .WithFooter("Zadnji update: ")
                .AddField("Prosle Nedelje", "<Ime> <Discord> & <Ime> <Discord>")
                .AddField("Ove Nedelje", "<Ime> <Discord> & <Ime> <Discord>")
                .AddField("Sledece Nedelje", "<Ime> <Discord> & <Ime> <Discord>")
                .WithCurrentTimestamp();

            var msg = await ReplyAsync("", false, embed.Build());

            File.WriteAllText(HallMonitorModule.HallMonitorMessageIDFilePath, msg.Id.ToString());
            HallMonitorModule.HallMonitorMessageID = msg.Id;
        }
    }
}