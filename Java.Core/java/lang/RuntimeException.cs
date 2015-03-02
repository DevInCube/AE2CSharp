using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java.lang
{
    public class RuntimeException : Exception
    {

        public RuntimeException() { }

        public RuntimeException(String msg) : base(msg) { }
    }
}
