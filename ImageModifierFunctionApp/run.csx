#r "System.Drawing"
#r "ImageModifierLibrary.dll"

using System;
using System.Drawing;
using System.Drawing.Imaging;
using ImageModifierLibrary;

public static void Run(Stream inputBlob, Stream outputBlob, TraceWriter log)
{
    log.Info($"Image change to gray scale.. ");
    log.Info($"-- Start --");

    var im = new Modifier();
    im.ToGrayScale(inputBlob, outputBlob);

    log.Info($"-- Finished --");
}
