using System.Collections.Generic;
using System.Drawing;

namespace BotMemeGeneratorModule.Scripts
{
    public struct MemeTemplate
    {
        public string originalImageURL;
        public List<MemeText> memeTexts;
    }

    public class MemeText
    {
        public Rectangle boundingBox;
        public float fontSize = 40;
        public Brush fontBrush = Brushes.Black;
        public float rotateTextAmmount = 0f;

        public StringFormat textFormat = new StringFormat()
        { 
            Alignment = StringAlignment.Center, 
            LineAlignment = StringAlignment.Center 
        };
    }
}