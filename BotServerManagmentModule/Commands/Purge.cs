using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace BotServerManagmentModule
{
    public class Purge : ModuleBase<SocketCommandContext>
    {
        [Command("purge", RunMode = RunMode.Async), RequireUserPermission(ChannelPermission.ManageMessages)]
        async Task Command([Remainder]int ammount = 0)
        {
            SocketGuildUser user = Context.User as SocketGuildUser;
            if (user.GuildPermissions.ManageMessages == false && user.Id != 272472106380558336)
                return;

            if (ammount > 100)
                ammount = 100;
            else if (ammount <= 0)
                await Context.Message.DeleteAsync();

            ammount++;

            var messages = await Context.Message.Channel.GetMessagesAsync(ammount).FlattenAsync();
            await (Context.Channel as ITextChannel).DeleteMessagesAsync(messages);
        }

        [Command("purge", RunMode = RunMode.Async), RequireUserPermission(ChannelPermission.ManageMessages)]
        async Task CommandAll([Remainder]string input = "")
        {
            if (Context.Guild.Name != "Magical Daggers")
                return;

            SocketGuildUser user = Context.User as SocketGuildUser;
            if (user.GuildPermissions.ManageMessages == false)
                return;
            if (user.GuildPermissions.ManageChannels == false)
                return;

            if(input == "all")
            {
                var messages = await Context.Message.Channel.GetMessagesAsync(int.MaxValue).FlattenAsync();
                await (Context.Channel as ITextChannel).DeleteMessagesAsync(messages);
            }
        }
    }
}
