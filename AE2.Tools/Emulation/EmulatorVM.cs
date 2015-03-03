using javax.microedition.lcdui;
using javax.microedition.midlet;
using MIDP.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java.csharp;
using System.Windows.Input;
using MIDP.WPF.Views;

namespace AE2.Tools.Emulation
{
    public class EmulatorVM : ObservableObject
    {

        private IEventSource eventSource;

        private System.Windows.Controls.Control _Control;

        public System.Windows.Controls.Control Control
        {
            get { return _Control; }
            set { _Control = value; OnPropertyChanged("Control"); }
        }

        public EmulatorVM()
        {
            //
        }

        public void SetEventSource(IEventSource eventSource)
        {
            this.eventSource = eventSource;
        }

        internal void LoadMIDlet(MIDlet midlet)
        {
            midlet.getClass().setResourceLoader(new WPFResourceLoader("Resources"));
            Display display = Display.getDisplay(midlet);
            this.Control = display.Control;
            (display.Control as DisplayControl).SetEventSource(eventSource);
            midlet.startApp();
        }
    }
}
