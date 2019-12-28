using Bindings;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BotSchoolModule.Commands.Admin
{
    public class RemoveTest : ModuleBase<SocketCommandContext>
    {
        [Command("remove-test", RunMode = RunMode.Async)]
        public async Task CommandAsync(int id)
        {
            if (id < 0)
                return;

            var test = DatabaseController.DatabaseAccessors.Tests.GetTestByID((ulong)id);
            if (test == null)
            {
                await ReplyAsync($"Couldn't find a test with this ID: {id}");
                return;
            }

            DatabaseController.DatabaseAccessors.Tests.RemoveTestByID((ulong)id);

            var subjects = DatabaseController.DatabaseAccessors.Subjects.GetAllSubjects();
            await ReplyAsync("Succuessfully removed a Test with this info:\n" +
                $"ID: #{test.ID}\n" +
                $"SubjectID: {test.subjectID} ({subjects.FirstOrDefault(x => x.ID == test.subjectID).Name})\n" +
                $"TimeStamp: {test.timeStamp} ({new DateTime((long)test.timeStamp, DateTimeKind.Utc).ToLocalTime().ToString()})\n" +
                $"Type: {test.type} ({((TestType)test.type).ToString()})\n" +
                $"Comment: {test.comment}");
        }
    }
}
