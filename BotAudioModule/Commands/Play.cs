using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using Victoria;

using BotAudioModule;
using Victoria.Enums;

namespace BotAudioModule.Commands
{
    public class Play : ModuleBase<SocketCommandContext>
    {
        /*private readonly LavaNode _lavaNode;

        public Play(LavaNode lavaNode)
        {
            _lavaNode = lavaNode;
        }

        [Command("Play")]
        public async Task PlayAsync([Remainder] string query)
        {
            var search = await _lavaNode.SearchYouTubeAsync(query);
            var track = search.Tracks.FirstOrDefault();

            var player = _lavaNode.HasPlayer(Context.Guild)
                ? _lavaNode.GetPlayer(Context.Guild)
                : await _lavaNode.JoinAsync(((SocketGuildUser)Context.User).VoiceChannel, (ITextChannel)Context.Channel);

            if (player.PlayerState == PlayerState.Playing)
            {
                player.Queue.Enqueue(track);
                await ReplyAsync($"Enqeued {track.Title}.");
            }
            else
            {
                try
                {
                    await player.PlayAsync(track);
                }
                catch (Exception e)
                {
                    await ReplyAsync(e.ToString());
                    await ReplyAsync("Exception player.PlayAsync(track);");
                    await ReplyAsync((track == null).ToString());
                    await ReplyAsync((player == null).ToString());
                }
                await ReplyAsync($"Playing {track.Title}.");
            }
        }*/

        /*[Alias("request", "play", "p")]
        [Command("songrequest", RunMode = RunMode.Async)]
        async Task RequestYoutube([Remainder] string searchTerm = "")
        {
            if (ValidSearch(searchTerm))
                await Search(searchTerm, false);
        }

        [Alias("request-sc", "play-sc", "p-sc")]
        [Command("songrequest-soundcloud", RunMode = RunMode.Async)]
        async Task RequestSoundcloud([Remainder] string searchTerm = "")
        {
            if(ValidSearch(searchTerm))
                await Search(searchTerm, true);
        }

        bool ValidSearch(string searchTerm)
        {
            SocketGuildUser user = (SocketGuildUser)Context.User;

            if (user.VoiceChannel != null)
            {
                if (AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id) != null)
                {
                    if (user.VoiceChannel != AudioModule.audioService._lavalink.DefaultNode.GetPlayer(Context.Guild.Id).VoiceChannel)
                    {
                        ReplyAsync("You cannot perform this command while not in the voice channel!");
                        return false;
                    }
                }
            }
            else
            {
                ReplyAsync(":x: You Must First Join a Voice Channel");
                return false;
            }

            if (searchTerm == "")
            {
                ReplyAsync("Please input what you want to search for...");
                return false;
            }

            return true;
        }

        async Task Search(string searchTerm, bool soundcloud, bool options = false)
        {
            SocketGuildUser user = (SocketGuildUser)Context.User;

            try
            {
                Lavalink _lavalink = AudioModule.audioService._lavalink;
                var player = _lavalink.DefaultNode.GetPlayer(Context.Guild.Id);
                if (player == null)
                {
                    await _lavalink.DefaultNode.ConnectAsync(user.VoiceChannel, Context.Message.Channel);
                    AudioModule.audioService.Options.TryAdd(user.Guild.Id, new AudioOptions
                    {
                        Summoner = user
                    });
                    player = _lavalink.DefaultNode.GetPlayer(Context.Guild.Id);
                }

                LavaTrack track;
                LavaResult search;
                if (soundcloud)
                    search = await _lavalink.DefaultNode.SearchSoundcloudAsync(searchTerm);
                else
                    search = await _lavalink.DefaultNode.SearchYouTubeAsync(searchTerm);

                if (search == null || search.LoadResultType == LoadResultType.NoMatches)
                {
                    await ReplyAsync($"No matches found for `{searchTerm}` ... try again?");
                    return;
                }

                //TODO: Add a 1-5 list for the user to pick from. (Like Fredboat)
                track = search.Tracks.FirstOrDefault();

                EmbedBuilder builder;

                if (player.CurrentTrack != null && player.IsPlaying || player.IsPaused)
                {
                    player.Queue.Enqueue(track);

                    builder = new EmbedBuilder();
                    builder.WithTitle("Song Added To Queue")
                        .WithColor(AudioModule.messageColor)
                        .WithCurrentTimestamp()
                        .AddField(track.Author, $"[{track.Title}]({track.Uri})\n {track.Length.ToString(@"hh\:mm\:ss")}")
                        .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(track.Uri));

                    await ReplyAsync("", false, builder.Build());
                    return;
                }

                await player.PlayAsync(track);

                LavaTrack lp = player.CurrentTrack;

                builder = new EmbedBuilder();
                builder.WithTitle("Currently Playing")
                    .WithColor(AudioModule.messageColor)
                    .AddField(lp.Author, $"[{lp.Title}]({lp.Uri})\n" +
                    $"{lp.Position.ToString(@"hh\:mm\:ss")} - {lp.Length.ToString(@"hh\:mm\:ss")}")
                    .WithThumbnailUrl(AudioService.GetYoutubeVideoThumbnailLink(lp.Uri))
                    .WithCurrentTimestamp();

                await ReplyAsync("", false, builder.Build());
            }
            catch (Exception ex)
            {
                await ReplyAsync("Uh, oh ... ahem OOF ... an error occurred :\n```" + ex.ToString() + "```\n <@272472106380558336> ...?");
            }
        }*/
    }
}
