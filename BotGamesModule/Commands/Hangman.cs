using BotGamesModule.Scripts;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotGamesModule.Commands
{
    public class Hangman : TurnBasedGame
    {
        string word = "";
        List<char> guessedChars = new List<char>();
        List<char> correctChars = new List<char>();
        bool waitingForHostDM = true;

        bool gameStarted;

        [Command("game hangman", RunMode = RunMode.Async)]
        async Task BeginCommand(params SocketUser[] users)
        {
            var fullUsers = users.ToList();
            fullUsers.Insert(0, Context.User);

            await Begin(fullUsers.ToArray(), Context.User);
        }

        public override void GotMessage(SocketMessage arg, GameState gameState)
        {
            base.GotMessage(arg, gameState);
            if (gameStarted == false) return;

            if (waitingForHostDM)
            {
                Task.Run(async delegate
                {
                    if (await IsHostDM(gameState, arg.Channel))
                    {
                        var text = arg.Content.ToUpper();
                        var words = text.Split(' ');
                        if(words.Count() > 3)
                        {
                            await arg.Channel.SendMessageAsync("You can't pick a sentence longer than 3 words!");
                            return;
                        }

                        foreach (var word in words)
                        {
                            if(word.Length > 27)
                            {
                                await arg.Channel.SendMessageAsync("Nah nah nah, you cant put words longer than 27 characters in, no-one will guess that, cmon...");
                                return;
                            }

                            var ascii = CheckAsciiOnly(word.ToUpper());
                            if (ascii == false)
                            {
                                await arg.Channel.SendMessageAsync("Only ASCII characters are allowed!");
                                return;
                            }
                        }

                        waitingForHostDM = false;
                        word = arg.Content.ToUpper();
                        await (await gameState.Host.GetOrCreateDMChannelAsync()).SendMessageAsync("Send me a message of the sentence/word that you want to use");
                        await ReplyAsync("The game can begin now!");
                        gameState.NextUser(false);
                    }
                });

                return;
            }

            if(arg.Author.Id == gameState.CurrentUser.Id)
            {
                Task.Run(async delegate
                {
                    var text = arg.Content.ToUpper();
                    if (text.StartsWith("GUESS "))
                    {
                        if (word == text.Replace("GUESS ", ""))
                        {
                            await ReplyAsync($"Congrats {gameState.CurrentUser.Mention}! It was `{word.ToLower()}`");
                            EndGame(gameState.GameStateID);
                            return;
                        }
                        else
                        {
                            await ReplyAsync($"Nope, that isn't the word.");
                            return;
                        }
                    }
                    else
                    {
                        char c = ' ';
                        if (char.TryParse(text, out c) == false)
                        {
                            await ReplyAsync("Only 1 character please, if you want to try to guess the word, type `guess` before it!");
                            return;
                        }

                        if (guessedChars.Contains(c) || correctChars.Contains(c))
                        {
                            await ReplyAsync("Someone already tried that character...");
                            gameState.NextUser(false);
                            await  ReplyAsync($"Next player is {gameState.CurrentUser.Mention}");
                            return;
                        }

                        var wordChars = word.ToCharArray();
                        if (wordChars.Contains(c))
                            correctChars.Add(c);
                        else
                            guessedChars.Add(c);

                        await DrawHangman();
                    }

                    gameState.NextUser(false);
                    await ReplyAsync($"Next player is {gameState.CurrentUser.Mention}");
                });
            }
        }

        async Task DrawHangman()
        {
            string text = "";
            switch (guessedChars.Count)
            {
                case 0:
                    text = "```\n" +
                        "+---+\n" +
                        "  |   |\n" +
                        "      |\n" +
                        "      |\n" +
                        "      |\n" +
                        "      |\n" +
                        "=========```";
                    break;
                case 1:
                    text = "```\n" +
                        "+---+\n" +
                        "  |   |\n" +
                        "  O   |\n" +
                        "      |\n" +
                        "      |\n" +
                        "      |\n" +
                        "=========```";
                    break;
                case 2:
                    text = "```\n" +
                        "+---+\n" +
                        "  |   |\n" +
                        "  O   |\n" +
                        "  |   |\n" +
                        "      |\n" +
                        "      |\n" +
                        "=========```";
                    break;
                case 3:
                    text = "```\n" +
                        "+---+\n" +
                        "  |   |\n" +
                        "  O   |\n" +
                        " /|   |\n" +
                        "      |\n" +
                        "      |\n" +
                        "=========```";
                    break;
                case 4:
                    text = "```\n" +
                        "+---+\n" +
                        "  |   |\n" +
                        "  O   |\n" +
                        " /|\\  |\n" +
                        "      |\n" +
                        "      |\n" +
                        "=========```";
                    break;
                case 5:
                    text = "```\n" +
                        "+---+\n" +
                        "  |   |\n" +
                        "  O   |\n" +
                        " /|\\  |\n" +
                        " /    |\n" +
                        "      |\n" +
                        "=========```";
                    break;
                case 6:
                    text = "```\n" +
                         "+---+\n" +
                         "  |   |\n" +
                         "  O   |\n" +
                         " /|\\  |\n" +
                         " / \\  |\n" +
                         "      |\n" +
                         "=========```";
                    break;
            }

            text += "**Word:**\n";

            var wordChars = word.ToCharArray();
            text += "`";
            foreach (var character in wordChars)
            {
                if (correctChars.Contains(character))
                    text += character;
                else text += "_";
            }
            text += "`";

            text += "\n**Guessed Characters:**\n";

            foreach (var guessedChar in guessedChars)
            {
                text += guessedChar + " ";
            }

            await ReplyAsync(text);
        }

        public override void GameStarted()
        {
            base.GameStarted();
            gameStarted = true;
        }

        public override void End()
        {
            base.End();
            gameStarted = false;
        }

        public static bool CheckAsciiOnly(string str)
        {
            var allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var strChars = str.ToCharArray();

            foreach (var character in strChars)
                if (allowedCharacters.Contains(character) == false)
                    return false;
                
            return true;
        }
    }
}
