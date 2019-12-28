using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BotAudioModule.Scripts
{
    public class MusicCommand : ModuleBase<SocketCommandContext>
    {
        public AudioService audioService;

        public MusicCommand(AudioService audioService)
        {
            this.audioService = audioService;
        }

        public virtual async Task<bool> CheckVoiceChannel(bool checkPlaying = false, bool canConnect = false)
        {
            SocketGuildUser user = (SocketGuildUser)Context.User;
            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);

            if (player == null)
            {
                if (canConnect)
                {
                    await AudioService.LavaClient.ConnectAsync(user.VoiceChannel, (ITextChannel)Context.Message.Channel);
                    player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);

                    if (checkPlaying)
                        return false;
                    else return true;
                }
                else
                {
                    await Context.Channel.SendErrorEmbed("Bot isn't in a Voice Channel!");
                    return false;
                }
            }
            else if (checkPlaying && player.IsPlaying == false)
            {
                await Context.Channel.SendErrorEmbed("Bot isn't playing anything!");
                return false;
            }

            if (user.VoiceChannel == null || user.VoiceChannel != AudioService.LavaClient.GetPlayer(Context.Guild.Id).VoiceChannel)
            {
                await Context.Channel.SendErrorEmbed("You can't perform this command while not in a Voice Channel!");
                return false;
            }

            if(checkPlaying && player.CurrentTrack == null)
            {
                await Context.Channel.SendErrorEmbed("Bot isn't playing anything!");
                return false;
            }
            
            return true;
        }
    }
}
