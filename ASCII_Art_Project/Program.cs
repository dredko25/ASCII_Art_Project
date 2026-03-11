using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using NLog;



namespace ASCII_Art_Project
{
    public class Program
    {
        public static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        
        /// <summary>
        /// The entry point of the application that allows the user to select an image file, converts it to ASCII art, and saves the result to a text file. 
        /// </summary>
        /// <param name="args"> A string array that represents the command-line arguments. In this case, it is not used. </param>
        [STAThread]
        static void Main(string[] args)
        {
            Logger.Info("Starting the application.");

            OpenFileDialog openFileDilog = new OpenFileDialog
            {
                Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp"
            };

            byte counter = 0;

            do
            {
                if (openFileDilog.ShowDialog() != DialogResult.OK)
                {
                    Logger.Warn("No file selected.");
                    continue;
                }

                counter++;

                Logger.Info($"Selected file: {openFileDilog.FileName}");

                Console.Clear();

                try
                {
                    var fileName = openFileDilog.FileName;
                    var result = fileName.ProcessImage();

                    result.Reversed.SaveAsTextFile($"image{counter}.txt");
                    Logger.Info($"Image converted to ASCII and saved to image{counter}.txt");

                    foreach (var row in result.Normal)
                        Console.WriteLine(row);

                    Logger.Info($"User added {counter} images");

                    Console.WriteLine("Press enter to add new image. \n");
                    Console.ReadLine();

                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "An error occurred while processing the image.");
                }

            } while (true);
        }
    }
}
