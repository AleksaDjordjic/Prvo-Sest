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

            builder.WithTitle("Available Music Commands")
               .WithColor(AudioModule.messageColor)
               .WithDescription($"**Bot Prefix :** `{AudioModule.botPrefix}`\n" +
               $"**play/songrequest/request/p <url/searchTerm>** - Plays a Youtube Video with the specified URL or search term\n" +
               $"**play-sc/songrequest-soundcloud/request-sc/p-sc <url/searchTerm>** - Plays a Soundcloud Song with the specified URL or search term\n" +
               $"**loop-song** - Loops the currently playing song\n" +
               $"**queue <pageIndex>** - Shows the current queue\n" +
               $"**np/currentsong/songname/song/nowplaying** - Displays the currently playing song\n" +
               $"**pause** - Pauses the playback\n" +
               $"**resume** - Resumes the playback\n" +
               $"**volume <0-150>** - Changes the playback volume\n" +
               $"**forward/fwd/dw <timeInSeconds>** - Forwards the song by set seconds\n" +
               $"**seek <timeInSeconds>** - Seeks playback to the desired timestamp\n" +
               $"**skip/next/nextsong** - Skips the currently playing song\n" +
               $"**remove <songIndex>** - Removed a song from the queue with a specified index\n" +
               $"**move <oldIndex> <newIndex>** - Moves song's index from the queue from oldIndex to newIndex\n" +
               $"**clear** - Clears the remainder of the queue and stops the playback\n" +
               $"**join/summon** - Spawns the bot in to the voice channel and assigns it to the text channel\n" +
               $"**leave/dc/disconnect/quit** - Disconnects the bot")
               .WithCurrentTimestamp();

            await ReplyAsync("", false, builder.Build());
        }
    }
}
