using Discord.WebSocket;
using System.Collections.Generic;
using System.Linq;

namespace BotGamesModule.Scripts
{
    public class GameState
    {
        public static List<GameState> gameStates = new List<GameState>();

        static long lastGameID = 1;

        SocketUser host;
        List<SocketUser> awaitingConfirmation = new List<SocketUser>();

        int currentUserIndex = 0;
        List<SocketUser> users = new List<SocketUser>();

        public readonly long GameStateID;
        public readonly SocketGuild Guild;
        public readonly ISocketMessageChannel Channel;
        public SocketUser Host { get { return host; } }
        public SocketUser CurrentUser { get { return users[currentUserIndex]; } }
        public List<SocketUser> Users { get { return users; } }
        public List<SocketUser> UsersAwaingConfirmation { get { return awaitingConfirmation; } }
        public bool CanBegin { get { return awaitingConfirmation.Count == 0; } }

        public static GameState FromID(long gameStateID)
        {
            var states = gameStates.Where(x => x.GameStateID == gameStateID).ToList();

            if (states.Count >= 1)
                return states.First();
            else return null;
        }

        public GameState(SocketGuild guild, ISocketMessageChannel channel, List<SocketUser> users, out long gameStateID)
        {
            Guild = guild;
            Channel = channel;
            gameStateID = lastGameID++;
            GameStateID = gameStateID;
            gameStates.Add(this);

            this.users = users;
        }

        public void Start(List<SocketUser> users, SocketUser host)
        {
            awaitingConfirmation = users;
            awaitingConfirmation.Remove(host);
            this.host = host;
        }

        public void ConfirmUser(SocketUser user)
        {
            awaitingConfirmation.Remove(user);
        }

        public void NextUser(bool canBeHost)
        {
            currentUserIndex++;
            if(currentUserIndex >= users.Count)
            {
                if (canBeHost) currentUserIndex = 0;
                else currentUserIndex = 1;
            }
        }

        public void End()
        {
            gameStates.Remove(this);
        }
    }
}