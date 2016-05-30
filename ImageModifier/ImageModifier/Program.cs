using System;
using ImageModifierLibrary;
using System.IO;
using System.Reflection;

namespace ImageModifier
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("input: source image file full path: ");
                var sourceImagePath = Console.ReadLine();
                Console.Write("input: output folder path: ");
                var outputFolderPath = Console.ReadLine();

                var assemblydir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                using (var ss = new FileStream(sourceImagePath, FileMode.Open, FileAccess.Read))
                using (var overlayimg = new FileStream(Path.Combine(assemblydir,"demo.png"), FileMode.Open, FileAccess.Read))
                using (var xmlfile = new FileStream(Path.Combine(assemblydir,"haarcascade_frontalface_default.xml"), FileMode.Open, FileAccess.Read))
                using (var os = new FileStream(Path.Combine(outputFolderPath, "output.png"), FileMode.Create, FileAccess.ReadWrite))
                {
                    Console.WriteLine("Start..");
                    var im = new Modifier();
                    im.TransformFace(ss, os, overlayimg, xmlfile);
                    Console.WriteLine("Finished.");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error occurred: {0}", ex.Message);
            }
            Console.WriteLine("Hit any key.");
            Console.ReadKey();
        }
    }
}
