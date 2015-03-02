using java.lang;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java.lang
{
    public class Thread
    {

        private System.Threading.Thread thread;

        public Thread() { }
        public Thread(Runnable target)
        {
            this.thread = new System.Threading.Thread(target.run);
        }
        public Thread(Runnable target, String name) { }
        public Thread(String name) { }
        /*public Thread(ThreadGroup group, Runnable target) { }
        public Thread(ThreadGroup group, Runnable target, String name) { }
        public Thread(ThreadGroup group, Runnable target, String name, long stackSize) { }
        public Thread(ThreadGroup group, String name) { }*/

        public void start()
        {
            if (thread == null) throw new RuntimeException("Thread not created");
            thread.Start();
        }

        public static void sleep(int delay)
        {
            System.Threading.Thread.Sleep(delay);
        }
    }
}
