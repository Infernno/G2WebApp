using System;
using System.IO;

namespace G2WebApp.Core.FileSystem
{
    public abstract class File
    {
        public string Name { get; protected set; }
        public Stream Stream { get; protected set; }

        internal virtual string Path => Paths.DataFolderPath;

        public abstract string UrlPath { get; }
        public virtual long Size => Stream?.Length ?? 0;
        public virtual int BufferLength { get; protected set; } = 256;

        protected File() { }

        protected File(string Name, Stream Stream)
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException(nameof(Name));

            if (Stream == null)
                throw new ArgumentNullException(nameof(Stream));

            this.Name = Name;
            this.Stream = Stream;
        }

        public virtual void Upload(bool dispose = true)
        {
            if (Stream == null)
                throw new NullReferenceException(nameof(Stream));

            /*
            var buffer = new byte[BufferLength];
            var bytesRead = Stream.Read(buffer, 0, BufferLength);

            using (var Writer = System.IO.File.Create(Path + Name, (int)Size))
            {
                while (bytesRead > 0)
                {
                    Writer.Write(buffer, 0, bytesRead);
                    bytesRead = Stream.Read(buffer, 0, BufferLength);
                }

                Writer.Close();
                Stream.Close();

                Stream.Dispose();
            }
            */


            using (var fileStream = System.IO.File.Create(Path + Name))
            {
                Stream.Seek(0, SeekOrigin.Begin);
                Stream.CopyTo(fileStream);

                fileStream.Close();
                Stream.Close();
            }

            if (dispose)
                Stream.Dispose();
        }
    }
}
