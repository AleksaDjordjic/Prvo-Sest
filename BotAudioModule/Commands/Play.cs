using BotAudioModule.Scripts;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;
using Victoria.Entities;

namespace BotAudioModule.Commands
{
    public class Play : MusicCommand
    {
        public Play(AudioService audioService) : base(audioService)
        {
        }

        [Alias("request", "play", "p")]
        [Command("songrequest", RunMode = RunMode.Async)]
        async Task Request([Remainder]string searchTerm = "")
        {
            if (ValidSearch(searchTerm))
                await PlayQuery(searchTerm);
        }

        bool ValidSearch(string searchTerm)
        {
            SocketGuildUser user = (SocketGuildUser)Context.User;

            if (user.VoiceChannel != null)
            {
                if (AudioService.LavaClient.GetPlayer(Context.Guild.Id) != null &&
                    user.VoiceChannel != AudioService.LavaClient.GetPlayer(Context.Guild.Id).VoiceChannel)
                {
                    ReplyAsync("You cannot perform this command while not in a voice channel!");
                    return false;
                }
            }
            else
            {
                ReplyAsync(":x: You Must First Join a Voice Channel");
                return false;
            }

            if (searchTerm == "")
                return false;

            return true;
        }

        async Task PlayQuery(string query)
        {
            if (await CheckVoiceChannel(false, true) == false)
                return;

            SocketGuildUser user = (SocketGuildUser)Context.User;
            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);

            var search = await AudioService.LavaRestClient.SearchYouTubeAsync(query);
            if (search.LoadType == LoadType.NoMatches || search.LoadType == LoadType.LoadFailed)
            {
                await Context.Channel.SendErrorEmbed("No Matches Found!");
                return;
            }

            //TODO: Add a 1-5 list for the user to pick from. (Like Fredboat)
            var track = search.Tracks.FirstOrDefault();

            if(player.IsPlaying)
            {
                player.Queue.Enqueue(track);
                await Context.Channel.SendAddedToQueueEmbed(track);
            }
            else
            {
                await player.PlayAsync(track);
                await Context.Channel.SendNowPlayingEmbed(track);
            }
        }
    }
}
