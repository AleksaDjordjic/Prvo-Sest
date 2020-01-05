using BotMemeGeneratorModule.Scripts;
using Discord.Commands;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System;

namespace BotMemeGeneratorModule.Commands
{
    public class MakeMeme : ModuleBase<SocketCommandContext>
    {
        static readonly string memeDirectory = Directory.GetCurrentDirectory() + @"\memes\";

        [Command("make-meme", RunMode = RunMode.Async)]
        public async Task CommandTask(string memeKey, params string[] args)
        {
            if (Directory.Exists(memeDirectory) == false) Directory.CreateDirectory(memeDirectory);

            memeKey = memeKey.ToLower();
            if(TemplateManager.templates.ContainsKey(memeKey) == false)
            {
                await ReplyAsync("This meme template doesn't exist or the bot doesnt support it yet...");
                return;
            }

            var meme = TemplateManager.templates[memeKey];
            if(File.Exists(memeDirectory + memeKey + ".png") == false)
                using (WebClient wc = new WebClient())
                    wc.DownloadFile(meme.originalImageURL, memeDirectory + memeKey + ".png");

            using (Image img = Image.FromFile(memeDirectory + memeKey + ".png"))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    int counter = 0;
                    foreach (var text in meme.memeTexts)
                    {
                        Font preferedFont = new Font("Impact", text.fontSize, FontStyle.Regular);
                        StringFormat stringFormat = new StringFormat()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };

                        try
                        {
                            using(Font f = FindFont(g, args[counter], text.boundingBox.Width, text.boundingBox.Size, preferedFont))
                                g.DrawString(args[counter], f, Brushes.Black, text.boundingBox, stringFormat);
                        }
                        finally
                        {
                            preferedFont.Dispose();
                            counter++;
                        }
                    }
                }

                using (Bitmap bmp = new Bitmap(img))
                    bmp.Save(memeDirectory + $"{memeKey}-{Context.User.Id}-{DateTime.UtcNow.Ticks}-edit.png");

                await Context.Channel.SendFileAsync(memeDirectory + $"{memeKey}-{Context.User.Id}-edit.png");
            }
        }

        private Font FindFont(Graphics g, string longString, int maxWidth, Size Room, Font PreferedFont)
        {
            // you should perform some scale functions!!!
            SizeF RealSize = g.MeasureString(longString, PreferedFont, maxWidth);
            float HeightScaleRatio = Room.Height / RealSize.Height;
            float WidthScaleRatio = Room.Width / RealSize.Width;

            float ScaleRatio = (HeightScaleRatio < WidthScaleRatio)
               ? ScaleRatio = HeightScaleRatio
               : ScaleRatio = WidthScaleRatio;

            float ScaleFontSize = PreferedFont.Size * ScaleRatio;

            return new Font(PreferedFont.FontFamily, ScaleFontSize);
        }
    }
}
