using java.lang;

namespace java.io
{

    public class ByteArrayOutputStream : OutputStream
    {

        protected byte[] buf;
        protected int count;

        public ByteArrayOutputStream() { }

        public ByteArrayOutputStream(int paramInt) { }

        public byte[] toByteArray()
        {
            return null;
        }

        public int size()
        {
            return 0;
        }

        public String toString()
        {
            return null;
        }

        public override void close()
        { }

        public void reset() { }

        public override void write(byte[] bytearr, int off, int len) { }

        public override void write(int paramInt) { }

        public override void flush() { }
    }

}