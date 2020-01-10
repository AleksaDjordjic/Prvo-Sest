using BotMemeGeneratorModule.Scripts;
using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BotMemeGeneratorModule.Commands
{
    public class MemeTemplates : ModuleBase<SocketCommandContext>
    {
        const int itemsPerPage = 10;

        [Command("meme-templates", RunMode = RunMode.Async)]
        public async Task CommandTask([Remainder] int page = 1)
        {
            page--;

            if(page < 0)
            {
                await ReplyAsync("Funny page number ...");
                return;
            }

            var templates = TemplateManager.templates.OrderByDescending(x => x.Key).ToList();
            var startIndex = page * itemsPerPage;
            int endIndex = (page + 1) * itemsPerPage > templates.Count ? templates.Count : (page + 1) * itemsPerPage;

            if (startIndex > templates.Count)
            {
                await ReplyAsync($"This page does not exist, maximum is {Math.Ceiling((double)(TemplateManager.templates.Count / itemsPerPage))}");
                return;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithTitle($"Meme Templates {startIndex} - {endIndex - 1}")
                .WithColor(MemeGeneratorModule.messageColor)
                .WithCurrentTimestamp();

            for (int i = startIndex; i < endIndex; i++)
            {
                var template = templates[i];
                builder.AddField(template.Key,
                    $"Index: {i}\n" +
                    $"Image URL: {template.Value.originalImageURL}\n" +
                    $"Text Elements: {template.Value.memeTexts.Count}");
            }

            await ReplyAsync("", false, builder.Build());
        }

        [Command("meme-template", RunMode = RunMode.Async)]
        public async Task SpecificTemplate(int index)
        {
            var templates = TemplateManager.templates.OrderByDescending(x => x.Key).ToList();
            if (index < 0)
            {
                await ReplyAsync("Funny page number ...");
                return;
            }

            if(index >= templates.Count)
            {
                await ReplyAsync("Template at this index does not exist!");
                return;
            }

            var template = templates[index].Value;

            EmbedBuilder builder = new EmbedBuilder();
            builder
                .WithTitle($"Meme Template #{index}")
                .WithColor(MemeGeneratorModule.messageColor)
                .WithCurrentTimestamp()
                .WithDescription(
                    $"Index: {index}\n" +
                    $"Image URL: {template.originalImageURL}\n" +
                    $"Text Elements: {template.memeTexts.Count}")
                .WithImageUrl(template.originalImageURL);

            await ReplyAsync("", false, builder.Build());
        }
    }
}