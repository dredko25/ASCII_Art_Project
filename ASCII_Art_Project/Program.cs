using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.IO;


namespace ASCII_Art_Project
{
    internal class Program
    {
        private const double WIDTH_OFFSET = 1.7;
        private const int MAX_WIDTH = 474;

        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog openFileDilog = new OpenFileDialog
            {
                Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp"
            };

            do
            {
                if (openFileDilog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();

                Bitmap bitmap = new Bitmap(openFileDilog.FileName);
                bitmap = ResizeBitmap(bitmap);

                bitmap.ToGrayscale();

                var converter = new BitmapToASCIIConverter(bitmap);
                var rows = converter.Convert();

                foreach (var row in rows)
                    Console.WriteLine(row);

                var rowReversed = converter.ConvertReversed();
                File.WriteAllLines("image.txt", rowReversed.Select(r => new string(r)));
                
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Press enter to add new image... \n");
                Console.ReadLine();

            } while (true);
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH / bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
            return bitmap;
        }
    }
}
