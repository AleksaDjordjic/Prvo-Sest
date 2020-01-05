using System.Collections.Generic;
using System.Drawing;

namespace BotMemeGeneratorModule.Scripts
{
    public struct MemeTemplate
    {
        public string originalImageURL;
        public List<MemeText> memeTexts;
    }

    public struct MemeText
    {
        public Rectangle boundingBox;
        public float fontSize;
        public Brush fontBrush;
        public float rotateTextAmmount;
    }
}