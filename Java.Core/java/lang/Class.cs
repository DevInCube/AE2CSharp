using java.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java.csharp;

namespace java.lang
{
    public class Class
    {

        private ClassResourceLoader loader;

        public void setResourceLoader(ClassResourceLoader loader)
        {
            this.loader = loader;
        }

        public InputStream getResourceAsStream(String name)
        {
            if (loader == null) throw new RuntimeException("ClassResourceLoader not set");
            return loader.getResourceAsStrea(name);
        }
    }
}
