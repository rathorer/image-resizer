using Backend;
using System;
using System.IO;

namespace ImageResizerClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to image resizer.");
            Console.WriteLine("Please provide the image directory/folder which needs to be resized.");
            var folderLocation = Console.ReadLine();
            Console.WriteLine("Enter comma separated sizes in pixels. e.g. 200, 300, 400, 1080 etc. We will keep the image aspect reation same.");
            var sizes = Console.ReadLine();
            var imgTool = new ImgTool();
            
            if (!string.IsNullOrEmpty(sizes) && sizes.Length > 0) {
                var sizeArr = sizes.Split(new char[]{ ',' });
                var files = Directory.GetFiles(folderLocation, "*.jpg", SearchOption.TopDirectoryOnly);
                var numberOfImages = files.Length;
                foreach (var size in sizeArr)
                {
                    if (int.TryParse(size.Trim(), out int height)) {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Resizing images to ", height);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        foreach (var file in files)
                        {
                            imgTool.ResizeAndSaveImage(file, height);
                            Console.WriteLine(file.Substring(file.LastIndexOf('/')) + " done.");
                        }
                    }
                }
            }
            Console.Read();
        }
    }
}
