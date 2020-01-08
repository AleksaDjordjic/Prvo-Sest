using Discord.Commands;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using Discord;
using System.Collections.Generic;

namespace BotFunModule.Commands
{
    public class Reddit : ModuleBase<SocketCommandContext>
    {
        public static List<long> postedContent = new List<long>();

        [Command("reddit", RunMode = RunMode.Async)]
        public async Task CommandTask([Remainder]string subreddit = "programmerhumor")
        {
            var client = new RestClient("https://imgur.com");
            var request = new RestRequest("/r/{subreddit}/hot.json");
            request.AddUrlSegment("subreddit", subreddit);

            var _resp = client.Execute(request);

            var response = JsonConvert.DeserializeObject<RedditResponse>(_resp.Content);

            var random = new Random();
            long postID = 0;
            RedditPost post;

            do
            {
                post = response.data[random.Next(response.data.Length)];
                postID = post.id;
            }
            while (postedContent.Contains(postID) == true);

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithCurrentTimestamp()
                .WithColor(FunModule.messageColor)
                .WithTitle(post.title)
                .WithImageUrl($@"http://imgur.com/{post.hash}{post.ext}")
                .WithFooter($"By: {post.author} - {post.views} Views");

            await ReplyAsync("", false, builder.Build());
        }
    }
}
