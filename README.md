# image-resizer
Resizes multiple images in a folder at a time.

Built on .NET framework. 
Give the path of a directory/folder where bigger images are there, and give the desired size of images.

Steps:
1. Build the current solution, we have 2 versions of clients, one for .net core and other for .net framework. Backend is the common project used in both.
2. Running the project ImageResizerClientFramework using visual studio, or vs code, will open the command window.
3. Enter the location of directory/folder where bigger images are placed.
4. Provide the sizes (only one dimension (height), the second dimension i.e. width it will calculate automatically, to maintain the aspect ratio) of the you need, and press enter it will resize the bigger images to desired sizes and place them in a new folder creating at same place.
