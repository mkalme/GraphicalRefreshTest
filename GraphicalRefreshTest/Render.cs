using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphicalRefreshTest {
    public class Render {
        public int[] ColorArray { get; set; }
        private int Width { get; }
        private int Height { get; }
        public Bitmap Image { get; set; }

        public Render(int width, int height) {
            ColorArray = new int[width * height];
            Width = width;
            Height = height;
            Image = new Bitmap(width, height);
        }

        public void Clear(int color) {
            for (int i = 0; i < ColorArray.Length; i++) {
                ColorArray[i] = color;
            }
        }
        public void DrawBlock(Block block, int xp, int yp) {
            for (int y = 0; y < block.Height; y++) {
                int ya = yp + y;

                for (int x = 0; x < block.Width; x++) {
                    int xa = xp + x;

                    if (xa < 0 || xa >= Width || ya < 0 || ya >= Height) continue; 
                    ColorArray[xa + ya * Width] = block.ColorArray[x + y * block.Width];
                }
            }

            //for (int y = 0; y < block.Height; y++) {
            //    for (int x = 0; x < block.Width; x++) {
            //        int xL = xp + x;
            //        int yL = yp + y;

            //        if (xL >= 0 && xL < Width && yL >= 0 && yL < Height) {
            //            ColorArray[xL, yL] = block.ColorArray[x, y];
            //        }
            //    }
            //}
        }

        public void ConvertToImage() {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    Image.SetPixel(x, y, Color.FromArgb(ColorArray[x + y * Width]));
                }
            }
        }
        public void ConvertToArray(Bitmap image) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    ColorArray[x + y * Width] = image.GetPixel(x, y).ToArgb();
                }
            }
        }
    }
}
