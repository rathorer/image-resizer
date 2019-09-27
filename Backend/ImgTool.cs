using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class ImgTool
    {
        public bool ThumbnailCallback()
        {
            return false;
        }
        /// <summary>
        /// Get thumbnail of the image
        /// </summary>
        /// <param name="imgPath">path of actual image</param>
        /// <param name="oneSide"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public Image GetThumbnail(string imgPath, int oneSide = 0, double percentage = 0.0)
        {
            var sideMaxSize = 80.0;//just take 80 as default for one side
            Image.GetThumbnailImageAbort myCallback =
            new Image.GetThumbnailImageAbort(ThumbnailCallback);

            Image myBitmap = new Bitmap(imgPath);
            var widthHeightRatio = ((double)myBitmap.Width / (double)myBitmap.Height);
            if (percentage > 0)
            {
                sideMaxSize = (widthHeightRatio >= 1 ? (myBitmap.Width) : myBitmap.Height) * percentage / 100;
            }

            if (oneSide > 0)
            {
                sideMaxSize = oneSide;
            }

            int thumbnailWidth, thumbnailHeight;
            if (widthHeightRatio >= 1)
            {
                thumbnailWidth = (int)sideMaxSize;
                thumbnailHeight = (int)(1 / widthHeightRatio * sideMaxSize);
            }
            else
            {
                thumbnailWidth = (int)(sideMaxSize * widthHeightRatio);
                thumbnailHeight = (int)sideMaxSize;
            }

            //Image myThumbnail = myBitmap.GetThumbnailImage(
            //thumbnailWidth, thumbnailHeight, myCallback, IntPtr.Zero);

            Image myThumbnail = ResizeImage(myBitmap, thumbnailWidth, thumbnailHeight);
            //myThumbnail.Save(imgPath, ImageFormat.Jpeg);
            SaveThumbnail(myThumbnail, imgPath);
            myBitmap.Dispose();
            return myThumbnail;
            //e.Graphics.DrawImage(myThumbnail, 150, 75);
        }

        public Image GetThumbnail(string imgPath, int height, string name = null)
        {
            //var sideMaxSize = 80.0;//just take 80 as default for one side
            Image.GetThumbnailImageAbort myCallback =
            new Image.GetThumbnailImageAbort(ThumbnailCallback);

            Image myBitmap = new Bitmap(imgPath);
            var widthHeightRatio = ((double)myBitmap.Width / (double)myBitmap.Height);

            int thumbnailWidth, thumbnailHeight;
            if (myBitmap.Height < height)
            {
                thumbnailHeight = myBitmap.Height;
                thumbnailWidth = myBitmap.Width;
            }
            else
            {
                thumbnailHeight = height;
                thumbnailWidth = (int)(height * widthHeightRatio);
            }

            //Image myThumbnail = myBitmap.GetThumbnailImage(
            //thumbnailWidth, thumbnailHeight, myCallback, IntPtr.Zero);

            Image myThumbnail = ResizeImage(myBitmap, thumbnailWidth, thumbnailHeight);
            //myThumbnail.Save(imgPath, ImageFormat.Jpeg);
            if (string.IsNullOrEmpty(name)) {
                name = height.ToString();
            }
            SaveThumbnail(myThumbnail, imgPath, name);
            myBitmap.Dispose();
            return myThumbnail;
            //e.Graphics.DrawImage(myThumbnail, 150, 75);
        }

        public bool ResizeAndSaveImage(string imgPath, int height, string name = null)
        {
            var img = GetThumbnail(imgPath, height, name);
            return img.Height == height;
        }
        protected Image ResizeImage(Image image, int newWidth, int newHeight)
        {
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));
            }
            return newImage;
        }

        protected void SaveThumbnail(Image thumbnail, string imagePath)
        {
            var thumbnailDirectoryPath = Directory.GetParent(imagePath).FullName + Constants.ThumbnailDirectory;
            var fileName = imagePath.Remove(0, imagePath.LastIndexOf('\\') + 1);
            if (!Directory.Exists(thumbnailDirectoryPath))
            {
                DirectoryInfo thumbnailDirectory = Directory.CreateDirectory(thumbnailDirectoryPath);
                thumbnailDirectory.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            thumbnail.Save(thumbnailDirectoryPath + '\\' + fileName, ImageFormat.Jpeg);
        }

        protected void SaveThumbnail(Image thumbnail, string imagePath, string size)
        {
            var thumbnailDirectoryPath = Directory.GetParent(imagePath).FullName + "\\" + size;
            var fileName = imagePath.Remove(0, imagePath.LastIndexOf('\\') + 1);
            if (!Directory.Exists(thumbnailDirectoryPath))
            {
                DirectoryInfo thumbnailDirectory = Directory.CreateDirectory(thumbnailDirectoryPath);
                thumbnailDirectory.Attributes = FileAttributes.Directory;
            }

            thumbnail.Save(thumbnailDirectoryPath + '\\' + fileName, ImageFormat.Jpeg);
        }
    }
}
