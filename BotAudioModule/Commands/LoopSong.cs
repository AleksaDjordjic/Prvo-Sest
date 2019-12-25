using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotAudioModule.Commands
{
    public class LoopSong : ModuleBase<SocketCommandContext>
    {
        [Command("loop-song")]
        async Task CommandTask()
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
            
            if(player.CurrentTrack == null)
            {
                await ReplyAsync("Music is not playing ...");
                return;
            }

            bool currentLoop = AudioModule.audioService.loopCurrentSong;

            if(currentLoop)
                await ReplyAsync($"Disabled loop for `{player.CurrentTrack.Title}`");
            else
                await ReplyAsync($"Enabled loop for `{player.CurrentTrack.Title}`");

            AudioModule.audioService.loopCurrentSong = !AudioModule.audioService.loopCurrentSong;
        }
    }
}
