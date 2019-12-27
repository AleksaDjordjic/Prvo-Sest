using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class Subjects : ModuleBase<SocketCommandContext>
    {
        [Alias("subjects")]
        [Command("predmeti", RunMode = RunMode.Async)]
        public async Task CommandTask()
        {
            var subjects = DatabaseController.DatabaseAccessors.Subjects.GetAllSubjects();

            var description = "";
            foreach (var sub in subjects)
                description += $"`#{sub.ID}` - {sub.Name}\n";

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("Predmeti")
                .WithColor(Static.Color)
                .WithDescription(description)
                .WithCurrentTimestamp();

            await ReplyAsync("", false, embed.Build());
        }
    }
}