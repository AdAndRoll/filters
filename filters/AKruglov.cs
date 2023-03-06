using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace filters
{
    public class ReflectorFilter : filters
    {
        int R, G, B;
        bool IsPrepDone = false;
        protected bool Preparation(Bitmap Img)
        {
            R = G = B = 0;
            for (int i = 0; i < Img.Width; i++)
                for (int j = 0; j < Img.Height; j++)
                {
                    if (R < Img.GetPixel(i, j).R)
                        R = Img.GetPixel(i, j).R;
                    if (G < Img.GetPixel(i, j).G)
                        G = Img.GetPixel(i, j).G;
                    if (B < Img.GetPixel(i, j).B)
                        B = Img.GetPixel(i, j).B;
                }
            return true;
        }
        protected override Color MakeNewColor(Bitmap Img, int x, int y)
        {
            if (!IsPrepDone)
                IsPrepDone = Preparation(Img);
            Color SourceColor = Img.GetPixel(x, y);
            Color ResColor = Color.FromArgb(SourceColor.R * 255 / R, SourceColor.G * 255 / G, SourceColor.B * 255 / B);
            return ResColor;
        }
    }

    public class LinStretchFilter : filters
    {
        int R, G, B;
        int r, g, b;
        bool IsPrepDone = false;
        protected bool Preparation(Bitmap Img)
        {
            R = G = B = 0;
            r = g = b = 255;
            for (int i = 0; i < Img.Width; i++)
                for (int j = 0; j < Img.Height; j++)
                {
                    int cur = Img.GetPixel(i, j).R;
                    if (R < cur)
                        R = cur;
                    if (r > cur)
                        r = cur;
                    cur = Img.GetPixel(i, j).G;
                    if (G < cur)
                        G = cur;
                    if (g > cur)
                        g = cur;
                    cur = Img.GetPixel(i, j).B;
                    if (B < cur)
                        B = cur;
                    if (b > cur)
                        b = cur;
/*                    if (R < Img.GetPixel(i, j).R)
                        R = Img.GetPixel(i, j).R;
                    if (G < Img.GetPixel(i, j).G)
                        G = Img.GetPixel(i, j).G;
                    if (B < Img.GetPixel(i, j).B)
                        B = Img.GetPixel(i, j).B;
                    if (r > Img.GetPixel(i, j).R)
                        r = Img.GetPixel(i, j).R;
                    if (g > Img.GetPixel(i, j).G)
                        g = Img.GetPixel(i, j).G;
                    if (b > Img.GetPixel(i, j).B)
                        b = Img.GetPixel(i, j).B;
*/
                }
            return true;
        }
        protected override Color MakeNewColor(Bitmap Img, int x, int y)
        {
            if (!IsPrepDone)
                IsPrepDone = Preparation(Img);
            Color SourceColor = Img.GetPixel(x, y);
            Color ResColor = Color.FromArgb((SourceColor.R - r) * 255 / (R - r), (SourceColor.G - g) * 255 / (G - g), (SourceColor.B - b) * 255 / (B - b));
            return ResColor;
        }
    }
}