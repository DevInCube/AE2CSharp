namespace AE2.Tools.Emulation
{
    public class WPFResourceLoader : java.csharp.ClassResourceLoader
    {

        private readonly string _rootDir;

        public WPFResourceLoader(string rootDir)
        {
            this._rootDir = rootDir;
        }

        public override java.io.InputStream getResourceAsStrea(java.lang.String name)
        {
            var path = System.IO.Path.Combine(_rootDir, System.IO.Path.GetFileName(name.ToString()));
            var ms = new System.IO.MemoryStream();
            //using (ms)
            {
                using (var file = new System.IO.FileStream(
                    path: path,
                    mode: System.IO.FileMode.Open,
                    access: System.IO.FileAccess.Read))
                {
                    var bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int) file.Length);
                    ms.Write(bytes, 0, (int) file.Length);
                }
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return new java.io.DataInputStream(ms);
            }
        }
    }
}
