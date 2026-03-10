using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Art_Project
{
    public static class Extentions
    {

        /// <summary>
        /// Converts the bitmap to grayscale by averaging the red, green, and blue values of each pixel and setting the new color to a shade of gray based on that average for both console and file output.
        /// </summary>
        /// <param name="bitmap"> Object of type Bitmap that represents the image to be converted to grayscale. </param>
        public static void ToGrayscale(this Bitmap bitmap)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    var avg = (pixel.R + pixel.G + pixel.B) / 3;
                    bitmap.SetPixel(x, y, Color.FromArgb(pixel.A, avg, avg, avg));
                }
            }
        }
    }
}
