using System.Collections.Generic;
using System.Drawing;

namespace BotMemeGeneratorModule.Scripts
{
    public static class TemplateManager
    {
        public static readonly Dictionary<string, MemeTemplate> templates = new Dictionary<string, MemeTemplate>()
        {
            { "expanding-brain", new MemeTemplate()
            {
                originalImageURL = @"https://imgflip.com/s/meme/Expanding-Brain.jpg",
                memeTexts = new List<MemeText>()
                {
                    new MemeText()
                    {
                        boundingBox = new System.Drawing.Rectangle()
                        {
                            Height = 290,
                            Width = 420,
                            X = 0,
                            Y = 0
                        },
                        fontSize = 20,
                        fontBrush = Brushes.Black,
                        rotateTextAmmount = 0f
                    },
                    new MemeText()
                    {
                        boundingBox = new System.Drawing.Rectangle()
                        {
                            Height = 290,
                            Width = 420,
                            X = 0,
                            Y = 305
                        },
                        fontSize = 20,
                        fontBrush = Brushes.Black,
                        rotateTextAmmount = 0f
                    },new MemeText()
                    {
                        boundingBox = new System.Drawing.Rectangle()
                        {
                            Height = 290,
                            Width = 420,
                            X = 0,
                            Y = 610
                        },
                        fontSize = 20,
                        fontBrush = Brushes.Black,
                        rotateTextAmmount = 0f
                    },
                    new MemeText()
                    {
                        boundingBox = new System.Drawing.Rectangle()
                        {
                            Height = 290,
                            Width = 420,
                            X = 0,
                            Y = 895
                        },
                        fontSize = 20,
                        fontBrush = Brushes.Black,
                        rotateTextAmmount = 0f
                    }
                }
            }},
        };
    }
}
