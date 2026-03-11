using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ASCII_Art_Project
{
    public static class Extentions
    {
        private const double WIDTH_OFFSET = 1.7;
        private const int MAX_WIDTH = 474;

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


        /// <summary>
        /// Resizes the bitmap to fit within the specified maximum width while maintaining the aspect ratio.
        /// </summary>
        /// <param name="bitmap"> Object of type Bitmap that represents the image to be resized. </param>
        /// <returns> Object of type Bitmap that represents the resized image. </returns>
        public static Bitmap ResizeBitmap(this Bitmap bitmap)
        {
            var newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH / bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));

            return bitmap;
        }


        /// <summary>
        /// Saves the ASCII art represented as a 2D array of characters to a text file, where each row of the array is written as a new line in the file.
        /// </summary>
        /// <param name="asciiArt"> A 2D array of characters that represents the ASCII art to be saved to a text file. </param>
        /// <param name="filePath"> File path as a string that specifies the location and name of the text file where the ASCII art will be saved. </param>
        public static void SaveAsTextFile(this char[][] asciiArt, string filePath)
        {
            File.WriteAllLines(filePath, asciiArt.Select(r => new string(r)));
        }
}
