using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDP.WPF.ViewModels
{
    public interface IEventListener
    {

        void keyPressed(int keyCode);
        void keyReleased(int keyCode);
    }
}
