# image-resizer
# thumbnails creator
Resizes multiple images in a folder at a time. 

This can be used to creates thumbnails of desired sizes for bulk images, or can also be used to compress images by resizing them which fits the need. In most cases web application doesn't require very high resolution images or big sizes images, so resizing a image using this tool helps web applications to use compressed images.
If take a photo using your camera or phone, usually it comes with high resolution of greater than 4000x4000px, so when you use it for web or you want to share somehwere, 1300 x 700 or similar size is enough. So simply provide the size you need, and all of your images can be reszied in one go. Simply give the one side of image i.e. just width, so it will calculate height based on image aspect ratio.

Built on .NET framework. 
Give the path of a directory/folder where bigger images are there, and give the desired size of images.

Steps:
1. Clone the repo in your local, open it using visual studio.
2. Build the current solution, we have 2 versions of clients, one for .net core and other for .net framework. Backend is the common project used in both.
3. Running the project ImageResizerClientFramework using visual studio, or vs code, will open the command window.
4. Enter the location of directory/folder where bigger images are placed.
5. Provide the sizes (only one dimension (height), the second dimension i.e. width it will calculate automatically, to maintain the aspect ratio) of the you need, and press enter it will resize the bigger images to desired sizes and place them in a new folder creating at same place.
