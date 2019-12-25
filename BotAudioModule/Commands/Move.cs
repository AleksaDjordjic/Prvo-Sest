using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;


namespace BotAudioModule.Commands
{
    public class Move : ModuleBase<SocketCommandContext>
    {
        [Command("move")]
        async Task CommandTask(int oldIndex, int newIndex)
        {
            SocketGuildUser executionUser = (SocketGuildUser)Context.User;
            if (AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id).VoiceChannel == null)
            {
                await ReplyAsync("Bot isn't connected to a channel at the moment...");
                return;
            }

            if (executionUser.VoiceChannel == null || executionUser.VoiceChannel != AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id).VoiceChannel)
            {
                await ReplyAsync("You cannot perform this command while not in the voice channel!");
                return;
            }

            var player = AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id);

            if (oldIndex < 1)
            {
                await ReplyAsync("Song index cannot be bellow `1`");
                return;
            }

            if (oldIndex > player.Queue.Count)
            {
                await ReplyAsync($"Song at index of '{oldIndex}' does not exist");
                return;
            }

            if (newIndex > player.Queue.Count)
            {
                await ReplyAsync($"New index cannot be higher than the queue length");
                return;
            }

            if (oldIndex == newIndex)
            {
                await ReplyAsync("Indexes cannot be the same :expressionless:");
                return;
            }

            var items = player.Queue.Items.ToList();

            var toReaarange = items[oldIndex - 1];
            items.RemoveAt(oldIndex - 1);
            items.Insert(newIndex - 1, toReaarange);

            player.Queue.Clear();
            foreach (var item in items)
                player.Queue.Enqueue(item);

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Song Move")
                .WithCurrentTimestamp()
                .WithColor(AudioModule.messageColor)
                .WithDescription("Song has been successfully moved");

            await ReplyAsync("", false, builder.Build());
        }
    }
}
