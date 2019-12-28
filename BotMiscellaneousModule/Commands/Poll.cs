using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace BotMiscellaneousModule.Commands
{
    public class Poll : ModuleBase<SocketCommandContext>
    {
        [Alias("anketa")]
        [Command("poll", RunMode = RunMode.Async)]
        public async Task CommandTask(string title, [Remainder]string input)
        {
            string[] options = input.Split(';');
            if (options.Length > 10)
                await ReplyAsync("Izabrali ste vise od 10 opcija, lista je skracena na 10 opcija!");

            string text = "";

            int displayed = 1;
            foreach (var option in options)
            {
                if (displayed > 10)
                    continue;

                switch (displayed)
                {
                    case 1:
                        text += "1⃣";
                        break;
                    case 2:
                        text += "2⃣";
                        break;
                    case 3:
                        text += "3⃣";
                        break;
                    case 4:
                        text += "4⃣";
                        break;
                    case 5:
                        text += "5⃣";
                        break;
                    case 6:
                        text += "6⃣";
                        break;
                    case 7:
                        text += "7⃣";
                        break;
                    case 8:
                        text += "8⃣";
                        break;
                    case 9:
                        text += "9⃣";
                        break;
                    case 10:
                        text += "🔟";
                        break;
                }

                text += $" - {option}\n";
                displayed++;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithTitle(title)
                .WithColor(MiscellaneousModule.messageColor)
                .WithDescription(text)
                .WithFooter($"Poll od {Context.User}", Context.User.GetAvatarUrl());

            var message = await Context.Guild.GetTextChannel(660281952301219862).SendMessageAsync("", false, builder.Build());

            for (int i = 1; i < options.Length + 1; i++)
            {
                if (i > 10)
                    continue;

                switch (i)
                {
                    case 1:
                        await message.AddReactionAsync(new Emoji("1⃣"));
                        break;
                    case 2:
                        await message.AddReactionAsync(new Emoji("2⃣"));
                        break;
                    case 3:
                        await message.AddReactionAsync(new Emoji("3⃣"));
                        break;
                    case 4:
                        await message.AddReactionAsync(new Emoji("4⃣"));
                        break;
                    case 5:
                        await message.AddReactionAsync(new Emoji("5⃣"));
                        break;
                    case 6:
                        await message.AddReactionAsync(new Emoji("6⃣"));
                        break;
                    case 7:
                        await message.AddReactionAsync(new Emoji("7⃣"));
                        break;
                    case 8:
                        await message.AddReactionAsync(new Emoji("8⃣"));
                        break;
                    case 9:
                        await message.AddReactionAsync(new Emoji("9⃣"));
                        break;
                    case 10:
                        await message.AddReactionAsync(new Emoji("🔟"));
                        break;
                }
            }

            await ReplyAsync("Poll je napravljen u <#660281952301219862>!");
        }
    }
}