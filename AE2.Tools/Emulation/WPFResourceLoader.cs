using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE2.Tools.Emulation
{
    public class WPFResourceLoader : java.csharp.ClassResourceLoader
    {

        private string rootDir;

        public WPFResourceLoader(string rootDir)
        {
            this.rootDir = rootDir;
        }

        public override java.io.InputStream getResourceAsStrea(java.lang.String name)
        {
            string path = System.IO.Path.Combine(rootDir, System.IO.Path.GetFileName(name.ToString()));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            using (System.IO.FileStream file = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                ms.Write(bytes, 0, (int)file.Length);
            }
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            return new java.io.DataInputStream(ms);
        }
    }
}
