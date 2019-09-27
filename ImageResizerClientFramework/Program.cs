using Backend;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizerClientFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any(x=> x.ToLower() != "repeating"))
            {
                Console.WriteLine("Welcome to image resizer.");
            }
            Console.WriteLine("Please provide the image directory/folder which needs to be resized:");
            var folderLocation = Console.ReadLine();
            if (!Directory.Exists(folderLocation)) 
            {
                Console.WriteLine("Invalid locatoin.");
                Main(new string[] { "repeating" });
                return;
            }
            Console.WriteLine("Enter comma separated sizes in pixels (desired height of image). e.g. 200, 300, 400, 1080 etc. We will keep the image aspect ratio same.");
            Console.WriteLine("Or choose from one/multiple of below values:");
            Console.WriteLine(string.Join(", ", Enum.GetNames(typeof(ImageSizes))));
            var sizes = Console.ReadLine();
            var imgTool = new ImgTool();

            if (!string.IsNullOrEmpty(sizes) && sizes.Length > 0)
            {
                var sizeArr = sizes.Split(new char[] { ',' });
                var files = Directory.GetFiles(folderLocation, "*.jpg", SearchOption.TopDirectoryOnly);
                var numberOfImages = files.Length;
                
                Console.WriteLine($"Resizing {files.Length} images from the {folderLocation} folder.");
                foreach (var size in sizeArr)
                {
                    ImageSizes enumHeight = 0;
                    if (int.TryParse(size.Trim(), out int height) || Enum.TryParse(size.Trim(), out enumHeight))
                    {
                        height = enumHeight == 0 ? height : (int)enumHeight;
                        var folderName = enumHeight == 0 ? height.ToString() : Enum.GetName(typeof(ImageSizes), enumHeight);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Resizing images to "+ height);
                        Console.ResetColor();
                        foreach (var file in files)
                        {
                            imgTool.ResizeAndSaveImage(file, height, folderName);
                            Console.WriteLine(file.Substring(file.LastIndexOf('\\') + 1) + " done.");
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Done resizing. Resized images are placed in folders {0} under directory {1}", sizes, folderLocation);
            Console.ResetColor();
            Console.WriteLine("Enter any key to exit.");
            Console.Read();
        }
    }
    enum ImageSizes
    {         
        Thumbnail = 100,
        XThumbnail = 200,
        XSmall = 400,
        Small = 600,
        Medium = 800,
        Large = 1200,
        XLarge = 2000,
        XXLarge = 3000
    }
}
