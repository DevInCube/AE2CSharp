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

namespace AE2.Tools.Emulation
{
    public class EmulatorVM : ObservableObject
    {


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

        internal void LoadMIDlet(MIDlet midlet)
        {
            midlet.getClass().setResourceLoader(new WPFResourceLoader("Resources"));
            Display display = Display.getDisplay(midlet);
            this.Control = display.Control;
            midlet.startApp();
        }
    }
}
