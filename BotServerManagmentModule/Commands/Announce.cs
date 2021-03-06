﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BotServerManagmentModule.Commands
{
    public class Announce : ModuleBase<SocketCommandContext>
    {
        [Command("announce"), RequireUserPermission(ChannelPermission.ManageMessages)]
        async Task CommandTask(ISocketMessageChannel channel, string title, [Remainder]string message)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(title)
                .WithColor(ServerManagmentModule.messageColor)
                .WithDescription(message)
                .WithFooter(ServerManagmentModule.name, ServerManagmentModule.announceImage)
                .WithCurrentTimestamp();

            await channel.SendMessageAsync("", false, builder.Build());
        }
    }
}
