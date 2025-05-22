using System.Drawing;

namespace GraphicalRefreshTest {
    public class Block {
        public int[] ColorArray { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public static Block DefaultBlock = new Block(Properties.Resources.Block);
        public static Block Brick = new Block(Properties.Resources.Brick);
        public static Block HiddenBlock = new Block(Properties.Resources.HiddenBlock);

        public Block(Bitmap image) {
            ColorArray = new int[image.Width * image.Height];
            Width = image.Width;
            Height = image.Height;

            ConvertToArray(image);
        }

        private void ConvertToArray(Bitmap image) {
            for (int y = 0; y < image.Height; y++) {
                for (int x = 0; x < image.Width; x++) {
                    ColorArray[ x + y * Width] = image.GetPixel(x, y).ToArgb();
                }
            }
        }
    }
}
