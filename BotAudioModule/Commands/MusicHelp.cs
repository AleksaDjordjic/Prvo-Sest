using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class MusicHelp : ModuleBase<SocketCommandContext>
    {
        [Command("help-music")]
        public async Task CommandTask()
        {
            EmbedBuilder builder = new EmbedBuilder();

            var prefix = AudioModule.botPrefix;

            builder.WithTitle($"Available Music Commands - Bot Prefix: `{prefix}`")
               .WithColor(AudioModule.messageColor)
               .WithDescription(
                "All commands and their parameters\n" +
                "Parameter is marked as <>, if it has \"\" before and after <>, you must also include them (\") in the command!\n\n" +

               $"`{prefix}play <url/searchTerm>` - Plays a Youtube Video with the specified URL or search term\n" +
               $"`{prefix}loop` - Loops the current queue\n" +
               $"`{prefix}loop-song` - Loops the currently playing song\n" +
               $"`{prefix}queue <pageIndex>` - Shows the current queue\n" +
               $"`{prefix}nowplaying` - Displays the currently playing song\n" +
               $"`{prefix}pause` - Pauses the playback\n" +
               $"`{prefix}resume` - Resumes the playback\n" +
               $"`{prefix}volume <5-150>` - Changes the playback volume\n" +
               $"`{prefix}forward <timeInSeconds>` - Forwards the song by set seconds\n" +
               $"`{prefix}seek <timeInSeconds>` - Seeks playback to the desired timestamp\n" +
               $"`{prefix}skip` - Skips the currently playing song\n" +
               $"`{prefix}remove <songIndex>` - Removed a song from the queue with a specified index\n" +
               $"`{prefix}move <oldIndex> <newIndex>` - Moves song's index from the queue from oldIndex to newIndex\n" +
               $"`{prefix}clear` - Clears the remainder of the queue and stops the playback\n" +
               $"`{prefix}join` - Spawns the bot in to the voice channel and assigns it to the text channel\n" +
               $"`{prefix}leave` - Disconnects the bot")
               .WithCurrentTimestamp();

            await ReplyAsync("", false, builder.Build());
        }
    }
}
