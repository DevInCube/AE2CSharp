using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDP.WPF.ViewModels
{
    public interface IEventSource
    {
        event Action<int> KeyPressed;
        event Action<int> KeyReleased;
    }
}
