﻿using java.lang;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java.lang
{
    public class StringBuffer : StringBuilder
    {
        private String p;


        public StringBuffer(string spriteId)
        {
            //
        }

        public StringBuffer()
        {
            // TODO: Complete member initialization
        }

        public StringBuffer(String p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }
        public void append(object p)
        {
            throw new NotImplementedException();
        }

        public int Length { get; set; }

        public void append(char p)
        {
            throw new NotImplementedException();
        }
    }
}
