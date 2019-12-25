using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;


namespace BotAudioModule.Commands
{
    public class Queue : ModuleBase<SocketCommandContext>
    {
        int songsPerPage = 15;

        [Command("queue")]
        async Task CommandTask([Remainder] int pages = 1)
        {
            var player = AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id);
            if (player.IsPlaying)
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("Current Queue")
                    .WithCurrentTimestamp()
                    .WithColor(AudioModule.messageColor)
                    .WithDescription("**Currently Playing :**")
                    .AddField(player.CurrentTrack.Author,
                    $"[{player.CurrentTrack.Title}]({player.CurrentTrack.Uri}) - {player.CurrentTrack.Position.ToString(@"hh\:mm\:ss")}");

                if(player.Queue.Count > 0)
                    builder.AddField("\u200b \n--------------------", "In the Queue :");

                TimeSpan totalTime = TimeSpan.FromMilliseconds(0);
                int trackIndex = 1;
                foreach (var track in player.Queue.Items)
                {
                    if (trackIndex > (songsPerPage * pages) - songsPerPage && trackIndex <= (songsPerPage * pages))
                    {
                        try
                        {
                            builder
                                .AddField($"**#{trackIndex}** *{track.Author}*",
                                $"[{track.Title}]({track.Uri}) - {track.Length.ToString(@"hh\:mm\:ss")}");
                        }
                        catch { }
                    }
                    totalTime += track.Length;
                    trackIndex++;
                }
                totalTime += (player.CurrentTrack.Length - player.CurrentTrack.Position);
                builder.WithFooter($"{trackIndex} Songs - {totalTime.ToString(@"hh\:mm\:ss")} Listen Time");

                await ReplyAsync("", false, builder.Build());
            }
            else
                await ReplyAsync("No music is playing, the queue is empty...");
        }
    }
}
