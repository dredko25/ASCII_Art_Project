using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASCII_Art_Project;
using System.Drawing;
using System.Linq;

namespace ASCII_Art_Project.UnitTests
{
    [TestClass]
    public class BitmapToASCIIConverterTests
    {
        [TestMethod]
        public void Map_ValueIsMapped_ReturnsMappedValue()
        {
            // Arrange
            var converter = new BitmapToASCIIConverter(null);
            float value = 128;
            float fromSource = 0;
            float toSource = 255;
            float fromTarget = 0;
            float toTarget = 9;
            // Act
            byte result = (byte)converter.Map(value, fromSource, toSource, fromTarget, toTarget);
            // Assert
            Assert.AreEqual(4, result);
        }


        [TestMethod]
        public void Reverse_ValueIsReversed_ReturnsReversedValue()
        {
            // Arrange
            var converter = new BitmapToASCIIConverter(null);
            char[] testArray = { 'a', 'b', 'c', 'd', 'e' };
            char[] expectedReversedArray = { 'e', 'd', 'c', 'b', 'a' };
            // Act
            char[] result = converter.Reverse(testArray);
            // Assert
            CollectionAssert.AreEqual(expectedReversedArray, result);
        }


        [TestMethod]
        public void Convert_BlackPixel_MapsToFirstCharInAsciiTable()
        {
            // Arrange
            char[] asciiTable = new char[] { '@', '#', ' ', '.' };
            var bitmap = new Bitmap(1, 1);
            bitmap.SetPixel(0, 0, Color.FromArgb(0, 0, 0));
            var converter = new BitmapToASCIIConverter(bitmap);
            // Act
            var result = converter.Convert(asciiTable);
            // Assert
            Assert.AreEqual('@', result[0][0]);
        }


        /* IntegrationTest */
        [TestMethod]
        public void Convert_ConvertPixsels_ReturnsExpectedCharacters()
        {
            // Arrange
            var bitmap = new Bitmap(2, 1);
            bitmap.SetPixel(0, 0, Color.FromArgb(0, 0, 0));
            bitmap.SetPixel(1, 0, Color.FromArgb(255, 255, 255));
            char[] asciiTable = { '#', '.' };
            var converter = new BitmapToASCIIConverter(bitmap);
            // Act
            char[][] result = converter.Convert(asciiTable);
            // Assert
            Assert.AreEqual('#', result[0][0]);
            Assert.AreEqual('.', result[0][1]);
        }
    }

    [TestClass]
    public class ExtentionsTests
    {
        [TestMethod]
        public void ResizeBitmap_BitmapIsResized_ReturnsTrue()
        {
            // Arrange
            var originalBitmap = new Bitmap(500, 500);
            // Act
            var resizedBitmap = originalBitmap.ResizeBitmap();
            // Assert
            Assert.IsTrue(resizedBitmap.Width <= 474);
        }


        [TestMethod]
        public void ToGrayscale_BitmapIsGrayscaled_ReturnsGrayscaledBitmap()
        {
            // Arrange
            var bitmap = new Bitmap(1, 1);
            bitmap.SetPixel(0, 0, Color.FromArgb(255, 100, 150, 200));
            // Act
            bitmap.ToGrayscale();
            var resultColor = bitmap.GetPixel(0, 0);
            // Assert
            Assert.AreEqual(resultColor.R, resultColor.G);
            Assert.AreEqual(resultColor.G, resultColor.B);
        }


        [TestMethod]
        public void SaveAsTextFile_ASCIIArtIsSaved_ReturnsTrue()
        {
            // Arrange
            char[][] testArt = new char[][]
            {
                new char[] { 't', 'a', 'r' },
                new char[] { 't', 'a', 'r' }
            };
            string filePath = "test_art.txt";
            // Act
            testArt.SaveAsTextFile(filePath);
            // Assert
            Assert.IsTrue(System.IO.File.Exists(filePath));
        }


        [TestMethod]
        public void SaveAsTextFile_ASCIIArtIsSaved_FileContentIsCorrect()
        {
            // Arrange
            char[][] testArt = new char[][]
            {
                new char[] { 't', 'a', 'r' },
                new char[] { 't', 'a', 'r' }
            };
            string filePath = "test_art.txt";
            // Act
            testArt.SaveAsTextFile(filePath);
            string fileContent = System.IO.File.ReadAllText(filePath);
            // Assert
            Assert.AreEqual("tar\r\ntar\r\n", fileContent);
        }


        [TestMethod]
        public void ProcessImage_ValidImage_ReturnsReversedAndNormal()
        {
            // Arrange
            var filePath = "E://Sys_files//Downloads/sheep.jpg";
            // Act
            var result = filePath.ProcessImage();
            var normalReversed = result.Normal.Reverse().ToArray();
            // Assert
            Assert.IsNotNull(result.Reversed);
            Assert.IsNotNull(result.Normal);
            Assert.AreEqual(normalReversed.Length, result.Reversed.Length);
        }
    }
}
