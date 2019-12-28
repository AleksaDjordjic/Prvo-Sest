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

            embed.WithTitle($"Pomoc - Bot Prefix: `{Static.Prefix}`")
                .WithColor(Static.Color)
                .WithDescription(
                "Sve komande i njihovi parametri\n" +
                "Parametar se oznacava sa <>, ako ima \"\" pre i posle <>, morate da to (\") stavite u komandu!\n\n" +
                
                $"`{Static.Prefix}predmeti` - Lista svih predmeta i njihovi indexi u bazi podataka\n" +
                $"`{Static.Prefix}ucenici` - Lista svih ucenika i njihovi indexa u bazi podataka\n" +
                $"`{Static.Prefix}ja` - Osnovne informacije o tebi...\n" +
                $"`{Static.Prefix}testovi` - Lista svih testova koje treba da imamo\n" +
                $"\n" +
                $"`{Static.Prefix}anketa \"<naslov>\" <opcije>` - Napravi novu anketu u <#660281952301219862> sa opcijama <opcije> (Maximum 10 opcija, svaka opcija je odvojena sa `;`. Primer: Prva Opcija;Druga Opcija;Treca Opcija\n" +
                $"\n" +
                $"`{Static.Prefix}reddit <subreddit>(default=programmerhumor)` - Nasumicna slika sa \"hot\" liste <subreddit> subreddit-a" +
                $"\n\n" +
                
                $"**Ostale Help Komande**\n" +
                $"`{Static.Prefix}help-music` - Lista svih komandi za pustanje muzike\n" +
                $"`{Static.Prefix}help-admin` - Lista svih komandi za admine")
                .WithCurrentTimestamp();

            await ReplyAsync("", false, embed.Build());
        }
    }
}