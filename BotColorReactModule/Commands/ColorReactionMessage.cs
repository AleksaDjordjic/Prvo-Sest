using Discord;
using Discord.Commands;
using System.IO;
using System.Threading.Tasks;

namespace BotColorReactModule.Commands
{
    public class ColorReactionMessage : ModuleBase<SocketCommandContext>
    {
        [Command("color-reaction-message", RunMode = RunMode.Async)]
        public async Task CommandTask()
        {
            if (Context.User.Id != 272472106380558336)
                return;

            EmbedBuilder embed = new EmbedBuilder();

            embed.WithColor(ColorReactModule.messageColor)
                .WithTitle("Odaberite Vasu Boju")
                .WithDescription("Samo reagujte na poruku sa jednom od dole ponudjenih boja i dobicete tu boju u chatu!\n\n" +
                "🟥 - Crvena\n" +
                "🟦 - Plava\n" +
                "🟩 - Zelena\n" +
                "🟨 - Zuta\n" +
                "🟧 - Narandzasta\n" +
                "🟫 - Braon\n" +
                "🟪 - Ljubicasta\n" +
                "⬛ - Crna\n" +
                "⬜ - Bela");

            var msg = await ReplyAsync("", false, embed.Build());

            await msg.AddReactionsAsync(new IEmote[]
            { 
                new Emoji("🟥"),
                new Emoji("🟦"),
                new Emoji("🟩"),
                new Emoji("🟨"),
                new Emoji("🟧"),
                new Emoji("🟫"),
                new Emoji("🟪"),
                new Emoji("⬛"),
                new Emoji("⬜")
            });

            File.WriteAllText(ColorReactModule.ColorReactionMessageIDFilePath, msg.Id.ToString());
            ColorReactModule.ColorReactionMessageID = msg.Id;
        }
    }
}
