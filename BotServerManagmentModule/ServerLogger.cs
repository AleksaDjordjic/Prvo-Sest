using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BotServerManagmentModule
{
    public class ServerLogger
    {
        ulong logChannelID;
        SocketTextChannel logChannel;

        public ServerLogger(DiscordSocketClient socketClient, ulong logChannelID)
        {
            this.logChannelID = logChannelID;
            socketClient.MessageDeleted += MessageDeleted;
            socketClient.MessageUpdated += MessageUpdated;
            socketClient.GuildMemberUpdated += GuildMemberUpdated;
            socketClient.UserBanned += UserBanned;
        }

        async Task MessageDeleted(Cacheable<IMessage, ulong> cachable, ISocketMessageChannel _channel)
        {
            var channel = _channel as SocketGuildChannel;
            var Guild = channel.Guild;

            if (logChannel == null)
                logChannel = Guild.GetTextChannel(logChannelID);

            if (cachable.HasValue == false)
            {
                //await logChannel.SendMessageAsync("Message deleted, data could not be retrieved, bot was probably offline while this message was initially sent...");
                return;
            }

            var message = cachable.Value;

            // WHILE WORKING ON THE BOT, REMOVE LATER ON
            if (message.Channel == logChannel)
                return;

            if (string.IsNullOrWhiteSpace(message.Content))
                return;

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithAuthor(message.Author)
                .WithColor(Color.Red)
                .WithDescription(
                    $"**Message send by** {message.Author.Username} **deleted in** <#{message.Channel.Id}>\n" +
                    cachable.Value.Content)
                .WithFooter($"Author: {message.Author.Id} | Message ID: {message.Id}")
                .WithCurrentTimestamp();

            await logChannel.SendMessageAsync("", false, builder.Build());
        }

        async Task MessageUpdated(Cacheable<IMessage, ulong> cachable, SocketMessage newMessage, ISocketMessageChannel _channel)
        {
            var channel = _channel as SocketGuildChannel;
            var Guild = channel.Guild;

            if (logChannel == null)
                logChannel = Guild.GetTextChannel(logChannelID);

            if (cachable.HasValue == false)
            {
                //await logChannel.SendMessageAsync("Message edited, data could not be retrieved, bot was probably offline while this message was initially sent...");
                return;
            }

            var oldMessage = cachable.Value;

            if (oldMessage.Content == newMessage.Content)
                return;

            if (string.IsNullOrWhiteSpace(oldMessage.Content) || 
                string.IsNullOrWhiteSpace(newMessage.Content))
                return;

            string _oldMsg;
            string _newMsg;

            if (oldMessage.Content.Length > 800)
                _oldMsg = oldMessage.Content.Substring(0, 800) + "...";
            else _oldMsg = oldMessage.Content;

            if (newMessage.Content.Length > 800)
                _newMsg = newMessage.Content.Substring(0, 800) + "...";
            else _newMsg = newMessage.Content;

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithAuthor(newMessage.Author)
                .WithColor(Color.DarkBlue)
                .WithDescription(
                    $"**Message edited in** <#{newMessage.Channel.Id}> [Jump to Message]({newMessage.GetJumpUrl()})")
                .AddField("Before", _oldMsg)
                .AddField("After", _newMsg)
                .WithFooter($"User ID: {newMessage.Author.Id}")
                .WithCurrentTimestamp();

            await logChannel.SendMessageAsync("", false, builder.Build());
        }

        async Task GuildMemberUpdated(SocketGuildUser oldUser, SocketGuildUser newUser)
        {
            if (oldUser.Nickname != newUser.Nickname)
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder
                    .WithAuthor(newUser)
                    .WithColor(Color.Orange)
                    .WithDescription(
                        $"**Server Nickname Changed** {newUser.Mention}")
                    .AddField("Before", string.IsNullOrEmpty(oldUser.Nickname) ? oldUser.Username : oldUser.Nickname)
                    .AddField("After", string.IsNullOrEmpty(newUser.Nickname) ? newUser.Username : newUser.Nickname)
                    .WithFooter($"User ID: {newUser.Id}")
                    .WithCurrentTimestamp();

                if (logChannel == null)
                    logChannel = newUser.Guild.GetTextChannel(logChannelID);

                await logChannel.SendMessageAsync("", false, builder.Build());
            }
        }

        async Task UserBanned(SocketUser user, SocketGuild guild)
        {
            if (logChannel == null)
                logChannel = guild.GetTextChannel(logChannelID);

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithAuthor(user)
                .WithColor(Color.DarkRed)
                .WithDescription($"{user.Mention} **has been banned**")
                .WithFooter($"User ID: {user.Id}")
                .WithCurrentTimestamp();

            await logChannel.SendMessageAsync("", false, builder.Build());
        }
    }
}
