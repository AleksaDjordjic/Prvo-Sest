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

            socketClient.ChannelCreated += ChannelCreated;
            socketClient.ChannelDestroyed += ChannelDestroyed;
            socketClient.ChannelUpdated += ChannelUpdated;

            socketClient.RoleCreated += RoleCreated;
            socketClient.RoleDeleted += RoleDeleted;
            socketClient.RoleUpdated += RoleUpdated;
        }

        async Task RoleUpdated(SocketRole arg1, SocketRole arg2)
        {
            if (IsLogChannelNULL()) return;

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithColor(arg2.Color)
                .WithDescription(
                    $"Role Created: {arg2.Name} ({arg2.Id})")
                // TODO: ADD PERMISSIONS
                .WithFooter("Created At:")
                .WithTimestamp(arg2.CreatedAt);

            await logChannel.SendMessageAsync("", false, builder.Build());
        }

        async Task RoleDeleted(SocketRole arg)
        {
            if (IsLogChannelNULL()) return;

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithColor(arg.Color)
                .WithDescription(
                    $"Role Deleted: {arg.Name} ({arg.Id})")
                // TODO: ADD PERMISSIONS
                .WithFooter("Created At:")
                .WithTimestamp(arg.CreatedAt);

            await logChannel.SendMessageAsync("", false, builder.Build());
        }

        async Task RoleCreated(SocketRole arg)
        {
            if (IsLogChannelNULL()) return;

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithColor(arg.Color)
                .WithDescription(
                    $"Role Created: {arg.Name} ({arg.Id})")
                // TODO: ADD PERMISSIONS
                .WithFooter("Created At:")
                .WithTimestamp(arg.CreatedAt);

            await logChannel.SendMessageAsync("", false, builder.Build());
        }

        async Task ChannelUpdated(SocketChannel arg1, SocketChannel arg2)
        {
            if (IsLogChannelNULL()) return;

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithColor(Color.Red)
                .WithDescription(
                    $"Channel Updated: <#{arg1.Id}> ({arg1.Id})")
                .WithFooter("Created At:")
                .WithTimestamp(arg1.CreatedAt);

            await logChannel.SendMessageAsync("", false, builder.Build());
        }

        async Task ChannelDestroyed(SocketChannel arg)
        {
            if (IsLogChannelNULL()) return;

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithColor(Color.Red)
                .WithDescription(
                    $"Channel Destroyed: <#{arg.Id}> ({arg.Id})")
                .WithFooter("Created At:")
                .WithTimestamp(arg.CreatedAt);

            await logChannel.SendMessageAsync("", false, builder.Build());
        }

        async Task ChannelCreated(SocketChannel arg)
        {
            if (IsLogChannelNULL()) return;

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithColor(Color.Blue)
                .WithDescription(
                    $"Channel Created: <#{arg.Id}> ({arg.Id})")
                .WithFooter("Created At:")
                .WithTimestamp(arg.CreatedAt);

            await logChannel.SendMessageAsync("", false, builder.Build());
        }

        async Task MessageDeleted(Cacheable<IMessage, ulong> cachable, ISocketMessageChannel _channel)
        {
            CheckLogChannel(_channel);

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
            CheckLogChannel(_channel);

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
            CheckLogChannel(oldUser.Guild);

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
            CheckLogChannel(guild);

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithAuthor(user)
                .WithColor(Color.DarkRed)
                .WithDescription($"{user.Mention} **has been banned**")
                .WithFooter($"User ID: {user.Id}")
                .WithCurrentTimestamp();

            await logChannel.SendMessageAsync("", false, builder.Build());
        }

        bool IsLogChannelNULL()
        {
            return logChannel == null;
        }

        void CheckLogChannel(ISocketMessageChannel _channel)
        {
            var channel = _channel as SocketGuildChannel;
            var Guild = channel.Guild;

            if (logChannel == null)
                logChannel = Guild.GetTextChannel(logChannelID);
        }

        void CheckLogChannel(SocketGuild guild)
        {
            if (logChannel == null)
                logChannel = guild.GetTextChannel(logChannelID);
        }
    }
}
