using java.lang;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java.io
{
    public abstract class InputStream
    {

        private  MemoryStream stream;

        public MemoryStream Stream { get { return stream; } }

        public InputStream(byte[] bytes)
        {
            stream = new MemoryStream(bytes);
        }

        public InputStream(InputStream stream)
        {
            this.stream = stream.stream;
        }

        public InputStream(MemoryStream fileStream)
        {
            this.stream = fileStream;
            this.stream.Seek(0, SeekOrigin.Begin);
        }

        public byte read()
        {
            return (byte)stream.ReadByte();
        }

        public void close()
        {
            stream.Close();
        }

    }
}
