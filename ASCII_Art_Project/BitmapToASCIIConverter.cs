using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Art_Project
{
    public class BitmapToASCIIConverter
    {
        private readonly Bitmap _bitmap;
        private readonly char[] _acsiiTable = { '.', ',', ':', '+', '*', '?', '%', 'S', '#', '@' };
        

        public BitmapToASCIIConverter(Bitmap bitmap)
        {
            _bitmap = bitmap;

        }


        /// <summary>
        /// Reverses the array to create a different visual effect in the resulting ASCII art in file.
        /// </summary>
        /// <returns> A new character array in reverse order. </returns>
        public char[] Reverse(char[] array)
        {
            char[] reversedTable = new char[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                reversedTable[i] = array[array.Length - 1 - i];
            }
            return reversedTable;
        }


        /// <summary>
        /// Converts the bitmap to a 2D array of characters based on the brightness of each pixel and the corresponding character in the ASCII table for console.
        /// </summary>
        /// <returns> Converted 2D array of characters. </returns>
        public char[][] Convert()
        {
            return Convert(_acsiiTable);
        }



        /// <summary>
        /// Converts the bitmap to a 2D array of characters based on the brightness of each pixel and the corresponding character in the reversed ASCII table for file.
        /// </summary>
        /// <returns> Converted 2D array of characters using the reversed ASCII table. </returns>
        public char[][] ConvertReversed()
        {
            return Convert(Reverse(_acsiiTable));
        }


        /// <summary>
        /// Converts the bitmap to a 2D array of characters based on the brightness of each pixel and the corresponding character in the provided ASCII table.
        /// </summary>
        /// <param name="asciiTable">An array of characters determined by the purpose of use.</param>
        /// <returns> Converted 2D array of characters using the provided ASCII table. </returns>
        public char[][] Convert(char[] asciiTable)
        {
            var result = new char[_bitmap.Height][];

            for (int y = 0; y < _bitmap.Height; y++)
            {
                result[y] = new char[_bitmap.Width];
                for (int x = 0; x < _bitmap.Width; x++)
                {
                    int mapIndex = (int)Map(_bitmap.GetPixel(x, y).R, 0, 255, 0, asciiTable.Length - 1);
                    result[y][x] = asciiTable[mapIndex];
                }
            }
            return result;
        }


        /// <summary>
        /// Maps a value from one range to another range. This is used to determine the appropriate index in the ASCII table based on the brightness of the pixel.
        /// </summary>
        /// <param name="valueToMap"> Starting value that needs to be mapped from the original range (0 to 255 for pixel brightness) to the target range (0 to length of ASCII table - 1). </param>
        /// <param name="start1"> Start of the original range (0 for pixel brightness). </param>
        /// <param name="stop1"> Stop of the original range (255 for pixel brightness). </param>
        /// <param name="start2"> Start of the target range (0 for ASCII table index). </param>
        /// <param name="stop2"> Stop of the target range (length of ASCII table - 1 for ASCII table index). </param>
        /// <returns> Value mapped from the original range to the target range. </returns>
        public float Map(float valueToMap, float start1, float stop1, float start2, float stop2)
        {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
    }
}
