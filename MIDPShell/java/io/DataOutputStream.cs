using java.lang;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java.io
{
    public class DataOutputStream : OutputStream
    {
        private ByteArrayOutputStream baos;

        public DataOutputStream(ByteArrayOutputStream baos)
        {
            // TODO: Complete member initialization
            this.baos = baos;
        }


        public void writeByte(byte p)
        {
           // throw new NotImplementedException();
        }

        public void writeShort(short p)
        {
           // throw new NotImplementedException();
        }

        public void writeInt(int p)
        {
           // throw new NotImplementedException();
        }

        public void close()
        {
            //throw new NotImplementedException();
        }

        public void writeUTF(String p)
        {
            throw new System.NotImplementedException();
        }
    }
}
