using System.Drawing.Imaging;
using System.IO;
using G2WebApp.Core.Extensions;

namespace G2WebApp.Core.FileSystem
{
    public class Image : File
    {
        internal override string Path => Paths.ImageFolderPath + "UserUpload" + System.IO.Path.DirectorySeparatorChar;

        public static int maxWidth { get; set; } = 600;
        public static int maxHeight { get; set; } = 600;

        public override string UrlPath => "/Images/UserUpload/" + Name;

        public Image(string fileName, Stream stream) : base(fileName, stream)
        {

        }

        public void Resize()
        {
            using (var img = System.Drawing.Image.FromStream(Stream))
            {
                if (img.Width <= maxWidth || img.Height <= maxHeight)
                    return;

                img.Resize(maxWidth, maxHeight).Save(Stream, ImageFormat.Jpeg);
            }
        }
    }
}
