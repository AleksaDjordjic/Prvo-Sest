using BotGamesModule.Scripts;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BotGamesModule.Commands
{
    public class TicTacToe : TurnBasedGame
    {
        static readonly string[] verticalValues = new string[] 
        {
            "1","2","3","4","5","6","8","9","10","11","12","13","14","15","16","17","18","19","20" 
        };
        static readonly string[] horizontalValues = new string[]
        {
            "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t"
        };

        [Command("game tictactoe", RunMode = RunMode.Async)]
        async Task BeginCommand(string settings, params SocketUser[] users)
        {
            var fullUsers = users.ToList();
            fullUsers.Insert(0, Context.User);

            if (fullUsers.Any(x => GameState.gameStates.Any(y => y.Users.Select(x => x.Id).Contains(x.Id))))
            {
                await ReplyAsync("The game can not begin, one of the users is already in a game!");
                return;
            }

            await Begin(fullUsers.ToArray(), Context.User);
        }

        public override void GotMessage(SocketMessage arg, GameState gameStateID)
        {
            base.GotMessage(arg, gameStateID);

            if (arg.Author.Id == gameState.CurrentUser.Id)
            {
                Task.Run(async delegate
                {
                    var text = arg.Content.ToLower();
                    if (text.Length > 3 || text.Length < 2) return;

                    var vertical = text.Substring(1);
                    var horizontal = text.Substring(0, 1);

                    if (verticalValues.Contains(vertical) == false ||
                        horizontalValues.Contains(horizontal)) return;
                });
            }
        }
    }
}
