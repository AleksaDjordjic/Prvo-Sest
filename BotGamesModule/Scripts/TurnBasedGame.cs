using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Discord;

namespace BotGamesModule.Scripts
{
    public class TurnBasedGame : ModuleBase<SocketCommandContext>
    {
        public GameState gameState;

        public async Task Begin(SocketUser[] players, SocketUser host)
        {
            long gameStateID = 0;
            GameState gs = new GameState(Context.Guild, Context.Message.Channel, players.ToList(), out gameStateID);
            gs.Start(players.ToList(), host);

            gameState = gs;

            await SendStartGameMessage(Context.Message.Channel, players, host);
            Context.Client.MessageReceived += async (arg) => { await Client_MessageReceived(arg, gameStateID); };
        }

        async Task Client_MessageReceived(SocketMessage arg, long gameStateID)
        {
            if (arg.Author.IsBot) return;

            var gameState = GameState.FromID(gameStateID);
            if (gameState == null) 
                return;

            var users = (await arg.Channel.GetUsersAsync().FlattenAsync()).ToList();
            var isPlayerDM = await IsPlayerDM(users, gameState.Users, arg.Channel.Id);

            if (isPlayerDM == false && gameState.Channel.Id != arg.Channel.Id)
                return;

            if (gameState.Users.Where(x => x.Id == arg.Author.Id).Count() < 1) 
                return;

            bool hasPlayerDeclined = false;
            if (CheckAllPlayersConfirmed(gameState, arg, out hasPlayerDeclined) == false) 
                return;

            if (hasPlayerDeclined) 
                return;

            GotMessage(arg, GameState.FromID(gameStateID));
        }

        bool CheckAllPlayersConfirmed(GameState gameState, SocketMessage message, out bool declined)
        {
            declined = false;

            if (gameState.UsersAwaingConfirmation.Where(x => x.Id == message.Author.Id).Count() >= 1)
            {
                if (message.Content.ToLower() == "play")
                {
                    gameState.ConfirmUser(message.Author);
                    _ = SendPlayersAwaiting(gameState.UsersAwaingConfirmation, message.Channel);

                    if(gameState.UsersAwaingConfirmation.Count < 1)
                    {
                        GameStarted();
                        return true;
                    }
                }
                else if(message.Content.ToLower() == "decline")
                {
                    _ = SendPlayerDeclined(message.Author, message.Channel);
                    End_(gameState.GameStateID);
                    declined = true;
                    return false;
                }
            }

            if (gameState.UsersAwaingConfirmation.Count >= 1) return false;
            else return true;
        }

        async Task SendPlayersAwaiting(List<SocketUser> usersAwaiting, ISocketMessageChannel channel)
        {
            if(usersAwaiting.Count >= 1)
            {
                var t = "Great, now we are just waiting for:\n";
                foreach (var user in usersAwaiting)
                    t += $"{user.Mention}\n";
                await channel.SendMessageAsync(t);
            }
            else
            {
                await channel.SendMessageAsync("Great, the game can start!");
            }
        }

        async Task SendPlayerDeclined(SocketUser user, ISocketMessageChannel channel)
        {
            await channel.SendMessageAsync($"Sadly, {user.Mention} declined your invite, the game can't start :(");
        }

        async Task SendStartGameMessage(ISocketMessageChannel channel, SocketUser[] players, SocketUser host)
        {
            var t = "The game is begining, everyone confirm your attendance!\n";
            foreach (var player in players)
            {
                if (player == host) continue;
                t += player.Mention + ", ";
            }
            t += "ready up by typing `play` or `decline` to cancel the game!";

            await channel.SendMessageAsync(t);
        }

        async Task<bool> IsPlayerDM(List<IUser> users, List<SocketUser> userIDs, ulong channelID)
        {
            foreach (var user in users)
            {
                if (userIDs.Select(x => x.Id).Contains(user.Id) == false) continue;
                var dm = await user.GetOrCreateDMChannelAsync();
                if (channelID == dm.Id) return true;
                else return false;
            }

            return false;
        }

        public async Task<bool> IsHostDM(GameState gs, ISocketMessageChannel channel)
        {
            return (await gs.Host.GetOrCreateDMChannelAsync()).Id == channel.Id;
        }

        public virtual void GameStarted()
        {

        }

        public virtual void GotMessage(SocketMessage arg, GameState gameStateID)
        {

        }

        public void EndGame(long gameStateID)
        {
            End_(gameStateID);
        }

        void End_(long gameStateID)
        {
            Context.Client.MessageReceived -= async (arg) => { await Client_MessageReceived(arg, gameStateID); };
            gameState.End();
            End();
        }

        public virtual void End()
        {

        }
    }
}