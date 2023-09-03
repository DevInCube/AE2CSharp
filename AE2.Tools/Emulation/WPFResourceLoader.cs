namespace AE2.Tools.Emulation
{
    public class WPFResourceLoader : java.csharp.ClassResourceLoader
    {
        private readonly string _rootDir;

        public WPFResourceLoader(string rootDir)
        {
            _rootDir = rootDir;
        }

        public override java.io.InputStream getResourceAsStrea(java.lang.String name)
        {
            var path = System.IO.Path.Combine(_rootDir, System.IO.Path.GetFileName(name.ToString()));
            var stream = System.IO.File.OpenRead(path);
            return new java.io.DataInputStream(stream);
        }
    }
}
