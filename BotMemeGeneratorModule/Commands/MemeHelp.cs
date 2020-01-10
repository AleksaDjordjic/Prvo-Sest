using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace BotMemeGeneratorModule.Commands
{
    public class MemeHelp : ModuleBase<SocketCommandContext>
    {
        [Command("meme-help", RunMode = RunMode.Async)]
        public async Task CommandTask()
        {
            EmbedBuilder builder = new EmbedBuilder();

            var prefix = MemeGeneratorModule.botPrefix;

            builder.WithTitle($"Available Music Commands - Bot Prefix: `{prefix}`")
               .WithColor(MemeGeneratorModule.messageColor)
               .WithDescription(
                "All commands and their parameters\n" +
                "Parameter is marked as <>, if it has \"\" before and after <>, you must also include them (\") in the command!\n\n" +

               $"`{prefix}make-meme <meme-key> \"<meme-text-1>\" \"<meme-text-2>\"....` Makes a meme from the specified Template with <meme-key> and specified texts <meme-text-n> depending on how many texts the template requires\n" +
               $"`{prefix}meme-templates <page>` Lists 10 meme templates from the <page> (Optional, default 1) displaying their Template URL, how many texts they require, index and meme-key\n" +
               $"`{prefix}meme-template <meme-index>` Displays more in-depth info about the meme template at the specific index, embeding the template as well")
               .WithCurrentTimestamp();

            await ReplyAsync("", false, builder.Build());
        }
    }
}