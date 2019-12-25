using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Alias("pomoc")]
        [Command("help", RunMode = RunMode.Async)]
        public async Task CommandAsync()
        {
            EmbedBuilder embed = new EmbedBuilder();

            embed.WithTitle($"Pomoc - Bot Prefix: {Static.Prefix}")
                .WithColor(Static.Color)
                .WithDescription(
                "Sve komande i njihovi parametri\n" +
                "Parametar se oznacava sa <>, ako ima \"\" pre i posle <>, morate da to stavite u komandu!\n\n" +
                
                $"`{Static.Prefix}predmeti` - Lista svih predmeta i njihovi indexi u bazi podataka\n\n" +
                
                $"**Ostale Help Komande**\n" +
                $"`{Static.Prefix}help-music` - Lista svih komandi za pustanje muzike\n" +
                $"`{Static.Prefix}help-admin` - Lista svih komandi za admine")
                .WithCurrentTimestamp();

            await ReplyAsync("", false, embed.Build());
        }
    }
}