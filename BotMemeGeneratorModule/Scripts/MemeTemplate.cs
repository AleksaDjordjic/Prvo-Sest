using System.Collections.Generic;
using System.Drawing;

namespace BotMemeGeneratorModule.Scripts
{
    public class MemeTemplate
    {
        public string originalImageURL;
        public List<MemeText> memeTexts;
    }

    public class MemeText
    {
        public Rectangle boundingBox;
        public float fontSize;
    }
}