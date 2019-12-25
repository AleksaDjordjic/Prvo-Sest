using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;

using BotAudioModule;
using Discord.WebSocket;

namespace BotAudioModule.Commands
{
    public class Remove : ModuleBase<SocketCommandContext>
    {
        [Command("remove")]
        async Task CommandTask(int index)
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

            Console.WriteLine(index);
            Console.WriteLine(player.Queue.Count);
            if (index > player.Queue.Count)
            {
                await ReplyAsync($"No song found with an index of {index}...");
            }
            else
            {
                var song = player.Queue.Items.ToList()[index - 1];
                player.Queue.RemoveAt(index - 1);
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("Song Removed From Queue")
                    .WithCurrentTimestamp()
                    .WithColor(AudioModule.messageColor)
                    .AddField(song.Title, $"{song.Author}\n{song.Uri}")
                    .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(song.Uri));

                AudioModule.audioService.loopCurrentSong = false;

                await ReplyAsync("", false, builder.Build());
            }
        }
    }
}
