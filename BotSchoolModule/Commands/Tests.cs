using Bindings;
using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BotSchoolModule.Commands
{
    public class Tests : ModuleBase<SocketCommandContext>
    {
        [Alias("testovi")]
        [Command("tests", RunMode = RunMode.Async)]
        public async Task CommandAsync()
        {
            var subjects = DatabaseController.DatabaseAccessors.Subjects.GetAllSubjects();
            var tests = DatabaseController.DatabaseAccessors.Tests.GetTestsFromTimeStamp((ulong)DateTime.UtcNow.Ticks);

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithColor(SchoolModule.messageColor)
                .WithTitle("Testovi")
                .WithDescription("Lista svih testova koje ce mo da imamo");

            foreach (var test in tests)
            {
                builder.AddField($"#{test.ID} {subjects.FirstOrDefault(x => x.ID == test.subjectID).Name}",
                    $"{test.type.TestTypeToString()} - {new DateTime((long)test.timeStamp, DateTimeKind.Utc).ToLocalTime()}\n" +
                    $"Dodatan komentar: {test.comment}");
            }

            await ReplyAsync("", false, builder.Build());
        }
    }
}
