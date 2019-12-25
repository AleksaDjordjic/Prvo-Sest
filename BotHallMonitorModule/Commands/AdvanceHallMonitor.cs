using DatabaseController.DatabaseAccessors;
using Discord;
using Discord.Commands;
using Discord.Rest;
using System.Linq;
using System.Threading.Tasks;

namespace BotHallMonitorModule.Commands
{
    public class AdvanceHallMonitor : ModuleBase<SocketCommandContext>
    {
        [Command("advance-hall-monitor", RunMode = RunMode.Async), RequireUserPermission(GuildPermission.Administrator)]
        public async Task CommandAsync()
        {
            if (HallMonitorModule.HallMonitorMessageID == 0)
                return;

            HallMonitorModule.CurrentWeek++;
            var msg = (RestUserMessage)(await Context.Guild.GetTextChannel(659343822127497216)
                .GetMessageAsync(HallMonitorModule.HallMonitorMessageID));

            var students = Students.GetAllStudents();
            var week = HallMonitorModule.CurrentWeek;

            var lastWeekStudentFirst = students.FirstOrDefault(x => x.ID == ((week - 1) * 2 % 20) - 1);
            var lastWeekStudentSecond = students.FirstOrDefault(x => x.ID == (week - 1) * 2 % 20);

            var thisWeekStudentFirst = students.FirstOrDefault(x => x.ID == (week * 2 % 20) - 1);
            var thisWeekStudentSecond = students.FirstOrDefault(x => x.ID == (week * 2 % 20));

            var nextWeekStudentFirst = students.FirstOrDefault(x => x.ID == ((week + 1) * 2 % 20) - 1);
            var nextWeekStudentSecond = students.FirstOrDefault(x => x.ID == (week + 1) * 2 % 20);

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("Redari")
                .WithColor(HallMonitorModule.messageColor)
                .WithDescription("Ovde mozete da vidite:\n" +
                "Ko bio redar\n" +
                "Ko je sada redar\n" +
                "Ko ce biti redar\n" +
                "3 u 1 <:emoji_9:659196392035713065>")
                .WithFooter("Zadnji update: ")
                .AddField("Prosle Nedelje", 
                    $"{lastWeekStudentFirst.FirstName} {lastWeekStudentFirst.LastName} (<@{lastWeekStudentFirst.DiscordID}>)" +
                    $" & " +
                    $"{lastWeekStudentSecond.FirstName} {lastWeekStudentSecond.LastName} (<@{lastWeekStudentSecond.DiscordID}>)")
                .AddField("Ove Nedelje",
                    $"{thisWeekStudentFirst.FirstName} {thisWeekStudentFirst.LastName} (<@{thisWeekStudentFirst.DiscordID}>)" +
                    $" & " +
                    $"{thisWeekStudentSecond.FirstName} {thisWeekStudentSecond.LastName} (<@{thisWeekStudentSecond.DiscordID}>)")
                .AddField("Sledece Nedelje",
                    $"{nextWeekStudentFirst.FirstName} {nextWeekStudentFirst.LastName} (<@{nextWeekStudentFirst.DiscordID}>)" +
                    $" & " +
                    $"{nextWeekStudentSecond.FirstName} {nextWeekStudentSecond.LastName} (<@{nextWeekStudentSecond.DiscordID}>)")
                .WithCurrentTimestamp();

            await msg.ModifyAsync(x => x.Embed = embed.Build());
            await ReplyAsync("Advanced...");
        }
    }
}