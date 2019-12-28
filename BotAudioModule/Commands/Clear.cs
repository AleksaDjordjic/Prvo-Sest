using BotAudioModule.Scripts;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Linq;
using Discord.WebSocket;

namespace BotAudioModule.Commands
{
    public class Clear : MusicCommand
    {
        public Clear(AudioService audioService) : base(audioService)
        {
        }

        [Alias("stop", "empty")]
        [Command("clear")]
        public async Task CommandTask()
        {
            if (await CheckVoiceChannel() == false)
                return;

            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);
            var channel = player.VoiceChannel;
            var users = (await channel.GetUsersAsync().FlattenAsync()).ToList();

            if(users.Count > 2 && ((SocketGuildUser)Context.User).GuildPermissions.Administrator == false)
            {
                await Context.Channel.SendErrorEmbed(
                    "There are multiple people in this Voice Channel, command has been disabled!\n" +
                    "You need to have Administrator Permissions to execute this command.");
                return;
            }

            if (player.IsPlaying)
                await player.StopAsync();

            player.Queue.Clear();
            await ReplyAsync("Playback stopped, queue cleared");
        }
    }
}
