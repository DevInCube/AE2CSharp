using java.lang;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java.csharp
{
    public static class Extensions
    {

        static Dictionary<object, Class> classes = new Dictionary<object, Class>();

        public static Class getClass(this object obj)
        {
            if (classes.ContainsKey(obj)) return classes[obj];
            Class cl = new Class();
            classes.Add(obj, cl);
            return cl;
        }
    }
}
