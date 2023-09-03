using System.IO;

namespace java.io
{
    public abstract class InputStream
    {

        private readonly MemoryStream _stream;

        public MemoryStream Stream => _stream;

        public InputStream(byte[] bytes)
        {
            _stream = new MemoryStream(bytes);
        }

        public InputStream(InputStream stream)
        {
            this._stream = stream._stream;
        }

        public InputStream(MemoryStream fileStream)
        {
            this._stream = fileStream;
            this._stream.Seek(0, SeekOrigin.Begin);
        }

        public byte read()
        {
            return (byte)_stream.ReadByte();
        }

        public void close()
        {
            _stream.Close();
        }

    }
}
