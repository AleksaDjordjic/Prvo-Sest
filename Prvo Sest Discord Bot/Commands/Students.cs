using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class Students : ModuleBase<SocketCommandContext>
    {
        [Alias("mucenici", "students", "mučenici", "učenici")]
        [Command("ucenici")]
        public async Task CommandTask()
        {
            var students = DatabaseController.DatabaseAccessors.Students.GetAllStudents();

            var description = "";
            foreach (var student in students)
                description += $"`#{student.ID}` - {student.FirstName} {student.LastName} <@{student.DiscordID}>\n";

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("Učenici")
                .WithColor(Static.Color)
                .WithDescription(description)
                .WithCurrentTimestamp();

            await ReplyAsync("", false, embed.Build());
        }

        [Alias("mucenk", "student", "mučenik", "učenik")]
        [Command("ucenik")]
        public async Task CommandTaskByID(int id)
        {
            if(id <= 0)
            {
                await ReplyAsync("Invalid ID!");
                return;
            }

            var student = DatabaseController.DatabaseAccessors.Students.GetStudentByID((ulong)id);
            if(student == null)
            {
                await ReplyAsync("Invalid ID!");
                return;
            }

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($"Učenik #{student.ID}")
                .WithColor(Static.Color)
                .WithDescription($"{student.FirstName} {student.LastName} - <@{student.DiscordID}>")
                .WithCurrentTimestamp();

            await ReplyAsync("", false, embed.Build());
        }
    }
}
