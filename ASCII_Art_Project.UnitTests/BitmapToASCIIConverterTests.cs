using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASCII_Art_Project;
using System.Drawing;

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
        public void ResizeBitmap_BitmapIsResized_ReturnsResizedBitmap()
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
    }
}
