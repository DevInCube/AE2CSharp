using java.lang;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java.io
{
    public class DataInputStream : InputStream
    {

        private System.IO.BinaryReader reader;

        public DataInputStream(InputStream stream) : base(stream)
        {
            reader = new System.IO.BinaryReader(Stream);            
        }

        public DataInputStream(System.IO.MemoryStream fileStream) : base(fileStream)
        {
            fileStream.Seek(0, System.IO.SeekOrigin.Begin);
            reader = new System.IO.BinaryReader(Stream);
        }

        public short readShort()
        {
            byte b1 = this.readByte();
            byte b2 = this.readByte();
            return (short)(b1 << 8 | b2);
        }

        public string readUTF()
        {
            short len = this.readShort();
            byte[] bytes = reader.ReadBytes(len);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public int readInt()
        {
            byte b1 = this.readByte();
            byte b2 = this.readByte();
            byte b3 = this.readByte();
            byte b4 = this.readByte();
            return (int)(b1 << 32 | b2 << 16 | b3 << 8 | b4);
        }

        public void readFully(byte[] p)
        {
            byte[] bs = this.reader.ReadBytes(p.Length);
            for (int i = 0; i < p.Length; i++)
                p[i] = bs[i];
        }

        public void close()
        {
            this.reader.Close();
        }

        public byte readByte()
        {
            return this.reader.ReadByte();
        }

        public void skip(int p)
        {
            while (p-- > 0)
                reader.ReadByte();
        }

        public long readLong()
        {
            return (long)reader.ReadInt64();
        }

        
    }
}
