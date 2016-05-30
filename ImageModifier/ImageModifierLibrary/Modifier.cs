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

        public void TransformFace(Stream input, Stream output, Stream overlayImage, Stream faceDetector)
        {
            using (var image = new Bitmap(input))
            using (var destimage = new Bitmap(input))
            using (var demoimage = new Bitmap(overlayImage))
            {
                var cascadeFace = Accord.Vision.Detection.Cascades.FaceHaarCascade.FromXml(faceDetector);
                var detectorFace = new Accord.Vision.Detection.HaarObjectDetector(cascadeFace);
                var faces = detectorFace.ProcessFrame(image);
                using (var g = Graphics.FromImage(destimage))
                {
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    foreach(var face in faces)
                    {
                        g.DrawImage(demoimage, face);
                    }
                    destimage.Save(output, ImageFormat.Png);
                }
            }

        }
    }
}
