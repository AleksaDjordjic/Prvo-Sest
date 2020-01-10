using BotMemeGeneratorModule.Scripts;
using Discord.Commands;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System;
using System.Linq;

namespace BotMemeGeneratorModule.Commands
{
    public class MakeMeme : ModuleBase<SocketCommandContext>
    {
        static readonly string memeDirectory = Directory.GetCurrentDirectory() + @"\memes\";

        [Command("make-meme", RunMode = RunMode.Async)]
        public async Task CommandTask(string memeKey, params string[] args)
        {
            await GenerateMeme(Context, false, memeKey, args);
        }

#if DEBUG
        [Command("make-meme-helpers", RunMode = RunMode.Async)]
        public async Task CommandTaskWithHelpers(string memeKey, params string[] args)
        {
            await GenerateMeme(Context, true, memeKey, args);
        }
#endif

        private async Task GenerateMeme(SocketCommandContext Context, bool drawBoundingBox, string memeKey, params string[] args)
        {
            if (Directory.Exists(memeDirectory) == false) Directory.CreateDirectory(memeDirectory);

            memeKey = memeKey.ToLower();
            if (TemplateManager.templates.ContainsKey(memeKey) == false)
            {
                await ReplyAsync("This meme template doesn't exist or the bot doesnt support it yet...");
                return;
            }

            var meme = TemplateManager.templates[memeKey];
            if (File.Exists(memeDirectory + memeKey + ".png") == false)
                using (WebClient wc = new WebClient())
                    wc.DownloadFile(meme.originalImageURL, memeDirectory + memeKey + ".png");

            var currentTicks = DateTime.UtcNow.Ticks;

            using (Image img = Image.FromFile(memeDirectory + memeKey + ".png"))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    for (int i = 0; i < meme.memeTexts.Count; i++)
                    {
                        var text = meme.memeTexts[i];

                        Font preferedFont = new Font("Impact", text.fontSize, FontStyle.Regular);

                        try
                        {
                            g.RotateTransform(text.rotateTextAmmount);

                            if (drawBoundingBox)
                                g.DrawRectangle(new Pen(Brushes.Red, 10), text.boundingBox);

                            using (Font f = FindFont(g, args[i], text.boundingBox.Width, text.boundingBox.Size, preferedFont))
                                g.DrawString(args[i], f, text.fontBrush, text.boundingBox, text.textFormat);

                            g.RotateTransform(-text.rotateTextAmmount);
                        }
                        finally
                        {
                            preferedFont.Dispose();
                        }
                    }
                }

                using (Bitmap bmp = new Bitmap(img))
                {
                    var codecInfo = ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.MimeType == "image/jpeg");
                    var encoder = Encoder.Compression;
                    var encoderParams = new EncoderParameters(1);

                    encoderParams.Param[0] = new EncoderParameter(encoder, 50);

                    bmp.Save(memeDirectory + $"{memeKey}-{Context.User.Id}-{currentTicks}-edit.jpeg", codecInfo, encoderParams);
                }

                await Context.Channel.SendFileAsync(memeDirectory + $"{memeKey}-{Context.User.Id}-{currentTicks}-edit.jpeg");
            }
        }

        private Font FindFont(Graphics g, string longString, int maxWidth, Size Room, Font PreferedFont)
        {
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
