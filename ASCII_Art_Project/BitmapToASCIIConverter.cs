using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Art_Project
{
    internal class BitmapToASCIIConverter
    {
        private readonly Bitmap _bitmap;
        private readonly char[] _acsiiTable = { '.', ',', ':', '+', '*', '?', '%', 'S', '#', '@' };
        
        public BitmapToASCIIConverter(Bitmap bitmap)
        {
            _bitmap = bitmap;

        }

        public char[] Reverse()
        {
            char[] reversedTable = new char[_acsiiTable.Length];
            for (int i = 0; i < _acsiiTable.Length; i++)
            {
                reversedTable[i] = _acsiiTable[_acsiiTable.Length - 1 - i];
            }
            return reversedTable;
        }

        public char[][] Convert()
        {
            return Convert(_acsiiTable);
        }

        public char[][] ConvertReversed()
        {
            return Convert(Reverse());
        }

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

        private float Map(float valueToMap, float start1, float stop1, float start2, float stop2)
        {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
    }
}
