using java.lang;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java.lang
{
    public class Thread
    {
        public Thread() { }
        public Thread(Runnable target) { }
        public Thread(Runnable target, String name) { }
        public Thread(String name) { }
        /*public Thread(ThreadGroup group, Runnable target) { }
        public Thread(ThreadGroup group, Runnable target, String name) { }
        public Thread(ThreadGroup group, Runnable target, String name, long stackSize) { }
        public Thread(ThreadGroup group, String name) { }*/

        public void start() { }

        public static void sleep(int delay)
        {
            throw new NotImplementedException();
        }
    }
}
