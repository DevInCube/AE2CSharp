using java.lang;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java.lang
{
    public class StringBuffer 
    {

        private StringBuilder builder;

        public StringBuffer()
        {
            builder = new StringBuilder();
        }

        public StringBuffer(String p) : this()
        {
            this.builder.Append(p.ToString());
        }
        public void append(object p)
        {
            builder.Append(p.ToString());
        }

        public void append(char p)
        {
            builder.Append(p);
        }

        public String toString()
        {
            return builder.ToString();
        }

        public int length()
        {
            return builder.Length;
        }
    }
}
