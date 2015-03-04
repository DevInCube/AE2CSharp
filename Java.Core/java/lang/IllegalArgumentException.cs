using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java.lang
{
    public class IllegalArgumentException : RuntimeException
    {

        public IllegalArgumentException() { }

        public IllegalArgumentException(String msg) : base(msg) { }
    }
}
