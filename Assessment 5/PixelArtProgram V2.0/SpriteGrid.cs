using System.Drawing;


namespace PixelArtProgram_V2._0
{
    public class SpriteGrid
    {
        public int GridWidth { get; set; } = 16;
        public int GridHeight { get; set; } = 16;
        public int Spacing { get; set; } = 1;

        public Image Image
        {
            get; private set;
        }

        public int Width
        {
            get
            {
                return (Image != null) ? Image.Width : 0;
            }
        }

        public int Height
        {
            get
            {
                return (Image != null) ? Image.Height : 0;
            }
        }
    }
}

