using Discord;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace BotColorReactModule
{
    public static class ReactionHandle
    {
        public static Task SocketClient_ReactionAdded(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (message.HasValue == false)
                return Task.CompletedTask;

            if (message.Value.Id != ColorReactModule.ColorReactionMessageID)
                return Task.CompletedTask;

            switch (reaction.Emote.Name)
            {
                case "🟥":
                    ((SocketGuildUser)reaction.User.Value)
                        .AddRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Red"));
                    break;
                case "🟦":
                    ((SocketGuildUser)reaction.User.Value)
                        .AddRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Blue"));
                    break;
                case "🟩":
                    ((SocketGuildUser)reaction.User.Value)
                        .AddRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Green"));
                    break;
                case "🟨":
                    ((SocketGuildUser)reaction.User.Value)
                        .AddRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Yellow"));
                    break;
                case "🟧":
                    ((SocketGuildUser)reaction.User.Value)
                        .AddRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Orange"));
                    break;
                case "🟫":
                    ((SocketGuildUser)reaction.User.Value)
                        .AddRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Brown"));
                    break;
                case "🟪":
                    ((SocketGuildUser)reaction.User.Value)
                        .AddRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Purple"));
                    break;
                case "⬛":
                    ((SocketGuildUser)reaction.User.Value)
                        .AddRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Black"));
                    break;
                case "⬜":
                    ((SocketGuildUser)reaction.User.Value)
                        .AddRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "White"));
                    break;
            }

            return Task.CompletedTask;
        }

        public static Task SocketClient_ReactionRemoved(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (message.HasValue == false)
                return Task.CompletedTask;

            if (message.Value.Id != ColorReactModule.ColorReactionMessageID)
                return Task.CompletedTask;

            switch (reaction.Emote.Name)
            {
                case "🟥":
                    ((SocketGuildUser)reaction.User.Value)
                        .RemoveRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Red"));
                    break;
                case "🟦":
                    ((SocketGuildUser)reaction.User.Value)
                        .RemoveRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Blue"));
                    break;
                case "🟩":
                    ((SocketGuildUser)reaction.User.Value)
                        .RemoveRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Green"));
                    break;
                case "🟨":
                    ((SocketGuildUser)reaction.User.Value)
                        .RemoveRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Yellow"));
                    break;
                case "🟧":
                    ((SocketGuildUser)reaction.User.Value)
                        .RemoveRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Orange"));
                    break;
                case "🟫":
                    ((SocketGuildUser)reaction.User.Value)
                        .RemoveRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Brown"));
                    break;
                case "🟪":
                    ((SocketGuildUser)reaction.User.Value)
                        .RemoveRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Purple"));
                    break;
                case "⬛":
                    ((SocketGuildUser)reaction.User.Value)
                        .RemoveRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "Black"));
                    break;
                case "⬜":
                    ((SocketGuildUser)reaction.User.Value)
                        .RemoveRoleAsync(
                            ((SocketGuildChannel)channel).Guild.Roles
                            .FirstOrDefault(x => x.Name == "White"));
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
