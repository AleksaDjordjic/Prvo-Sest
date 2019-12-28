using Bindings;
using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace BotSchoolModule.Commands.Admin
{
    public class AddTest : ModuleBase<SocketCommandContext>
    {
        [Command("add-test", RunMode = RunMode.Async), RequireUserPermission(ChannelPermission.ManageChannels)]
        public async Task CommandTask(DateTime date, int subject, int type, [Remainder]string comment = "")
        {
            if (subject < 0)
                return;
            if (string.IsNullOrEmpty(comment))
                comment = "Nema dodatnih komentara...";

            var subjects = DatabaseController.DatabaseAccessors.Subjects.GetAllSubjects();
            if (subjects.Where(x => x.ID == (ulong)subject).Count() <= 0)
            {
                await ReplyAsync($"Couldn't find a Subject with this ID: {subject}");
                return;
            }

            if (type < 0 || type > 1)
            {
                await ReplyAsync($"Invalid Test Type!\nUse 0 for `{(TestType)0}` and 1 for `{(TestType)1}`");
                return;
            }

            var utcDate = new DateTime(date.Ticks, DateTimeKind.Local).ToUniversalTime();

            var id = DatabaseController.DatabaseAccessors.Tests.AddTest(new Test
            {
                timeStamp = (ulong)utcDate.Ticks,
                subjectID = (ulong)subject,
                type = (TestType)type,
                comment = comment
            });

            var addedTest = DatabaseController.DatabaseAccessors.Tests.GetTestByID(id);

            await ReplyAsync("Added a new Test with this info:\n" +
                $"ID: #{addedTest.ID}\n" +
                $"SubjectID: {addedTest.subjectID} ({subjects.FirstOrDefault(x => x.ID == addedTest.subjectID).Name})\n" +
                $"TimeStamp: {addedTest.timeStamp} ({new DateTime((long)addedTest.timeStamp, DateTimeKind.Utc).ToLocalTime().ToString()})\n" +
                $"Type: {addedTest.type} ({((TestType)addedTest.type).ToString()})\n" +
                $"Comment: {addedTest.comment}");
        }
    }
}
