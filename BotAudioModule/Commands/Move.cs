using BotAudioModule.Scripts;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;


namespace BotAudioModule.Commands
{
    public class Move : MusicCommand
    {
        public Move(AudioService audioService) : base(audioService)
        {
        }

        [Command("move")]
        async Task CommandTask(int oldIndex, int newIndex)
        {
            if (await CheckVoiceChannel() == false)
                return;

            var player = AudioService.LavaClient.GetPlayer(Context.Guild.Id);

            if (oldIndex < 1)
            {
                await Context.Channel.SendErrorEmbed("Song index cannot be bellow `1`");
                return;
            }

            if (oldIndex > player.Queue.Count)
            {
                await Context.Channel.SendErrorEmbed($"Song at index of '{oldIndex}' does not exist");
                return;
            }

            if (newIndex > player.Queue.Count)
            {
                await Context.Channel.SendErrorEmbed($"New index cannot be higher than the queue length");
                return;
            }

            if (oldIndex == newIndex)
            {
                await Context.Channel.SendErrorEmbed("Indexes cannot be the same :expressionless:");
                return;
            }

            var items = player.Queue.Items.ToList();

            var toReaarange = items[oldIndex - 1];
            items.RemoveAt(oldIndex - 1);
            items.Insert(newIndex - 1, toReaarange);

            player.Queue.Clear();
            foreach (var item in items)
                player.Queue.Enqueue(item);

            await Context.Channel.SendSongMovedEmbed();
        }
    }
}
