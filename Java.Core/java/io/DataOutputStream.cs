using java.lang;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java.io
{
    public class DataOutputStream : OutputStream
    {

        private OutputStream stream;

        public DataOutputStream(OutputStream baos)
        {               
            this.stream = baos;
        }

        public void writeByte(byte p)
        {
            stream.write(p);
        }

        public void writeByte(sbyte p)
        {
            stream.write(p);
        }

        public void writeShort(short p)
        {
            stream.write(p >> 8 & 255);
            stream.write(p & 255);
        }

        public void writeInt(int p)
        {
            stream.write(p >> 24 & 255);
            stream.write(p >> 16 & 255);
            stream.write(p >> 8 & 255);
            stream.write(p & 255);
        }

        public void writeLong(long p)
        {
            //@todo
        }     

        public void writeUTF(String p)
        {
            string str = p.ToString();
            int length = p.length();
            if (length > 255) throw new System.NotImplementedException();
            //@todo
            byte strLen = (byte)length;
            stream.write(strLen);
            byte[] bytearr = new byte[strLen];
            int count = 0;
            for (byte x = 0; x < strLen; x++)
            {
                char c = str.ToCharArray()[x];
                if (!((c >= 0x0001) && (c <= 0x007F))) break;
                bytearr[count++] = (byte)c;
            }
            stream.write(bytearr);
        }

        public override void write(int b)
        {
            stream.write(b);
        }

        public override void close()
        {
            stream.close();
        }

        public override void flush()
        {
            stream.flush();
        }

        public override void write(byte[] bytearr)
        {
            foreach (byte b in bytearr)
                stream.write(b);
        }

        public override void write(byte[] bytearr, int off, int len)
        {
            for (int i = off; (off - i) < len; i++)
                stream.write(bytearr[i]);
        }
    }
}
