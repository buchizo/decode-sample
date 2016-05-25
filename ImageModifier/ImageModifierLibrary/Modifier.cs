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

        public void RunDecodedemo(Stream input, Stream output)
        {
            using (var image = new Bitmap(input))
            using (var destimage = new Bitmap(input))
            using (var demoimage = new Bitmap(@"demo.png",false))
            {
                var cascadeFace = Accord.Vision.Detection.Cascades.FaceHaarCascade.FromXml(@".\\haarcascade_frontalface_default.xml");
                var detectorFace = new Accord.Vision.Detection.HaarObjectDetector(cascadeFace);
                var faces = detectorFace.ProcessFrame(image);
                using (var g = Graphics.FromImage(destimage))
                {
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    var face = faces[0];
                    face.X = face.X - 40;
                    face.Y = face.Y - 30;
                    face.Size = new Size(face.Size.Width + 40, face.Size.Height + 60);
                    g.DrawImage(demoimage, face);
                    destimage.Save(output, ImageFormat.Png);
                }
            }

        }
    }
}
