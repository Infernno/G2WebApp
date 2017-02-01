using System.IO;

namespace G2WebApp.Core.FileSystem
{
    public static class Paths
    {
        public static string AppFolderPath => Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;

        public static string DataFolderPath => AppFolderPath + "Data" + Path.DirectorySeparatorChar;
        public static string ImageFolderPath => AppFolderPath + "Images" + Path.DirectorySeparatorChar;
    }
}
