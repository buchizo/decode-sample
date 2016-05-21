using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageModifierLibrary
{
    public class Modifier
    {
        public void ToGrayScale(Stream input, Stream output)
        {
            using (var image = new Bitmap(input))
            using (var graphicsImage = Graphics.FromImage(image))
            {
                var colorMatrix = new ColorMatrix(
                    new float[][]{
                    new float[]{0.299f, 0.299f, 0.299f, 0 ,0},
                    new float[]{0.587f, 0.587f, 0.587f, 0, 0},
                    new float[]{0.114f, 0.114f, 0.114f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                    });
                var imageAttribute = new ImageAttributes();
                imageAttribute.SetColorMatrix(colorMatrix);
                graphicsImage.DrawImage(image,
                    new Rectangle(0, 0, image.Width, image.Height),
                    0, 0, image.Width, image.Height,
                    GraphicsUnit.Pixel, imageAttribute);
                image.Save(output, ImageFormat.Png);
            }

        }
    }
}
