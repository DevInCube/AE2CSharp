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
using System.Windows;

namespace AE2.Tools.Emulation
{
    public class KeyCommand
    {
        public Key Key { get; set; }
        public ICommand Command { get; set; }

        public KeyCommand(Key Key, Action command)
        {
            this.Key = Key;
            this.Command = new RelayCommand(command);
        }
    }

    public class EmulatorVM : ObservableObject, IEventSource
    {
        public event Action<int> KeyPressed;
        public event Action<int> KeyReleased;
        public event Action ClosedAction;
        public Dictionary<string, KeyCommand> KeyDict { get; set; }

        private IEventSource eventSource;

        private System.Windows.Controls.Control _Control;

        public System.Windows.Controls.Control Control
        {
            get { return _Control; }
            set { _Control = value; OnPropertyChanged("Control"); }
        }

        public EmulatorVM()
        {
            KeyDict = new Dictionary<string, KeyCommand>();
            KeyDict.Add("L", new KeyCommand(Key.F1, () => { OnKeyPressed(-6); }));
            KeyDict.Add("R", new KeyCommand(Key.F2, () => { OnKeyPressed(-7); }));
            KeyDict.Add("0", new KeyCommand(Key.NumPad0, () => { OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM0); }));
            KeyDict.Add("1", new KeyCommand(Key.NumPad7, () => { OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM1); }));
            KeyDict.Add("2", new KeyCommand(Key.NumPad8, () => { OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM2); }));
            KeyDict.Add("3", new KeyCommand(Key.NumPad9, () => { OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM3); }));
            KeyDict.Add("4", new KeyCommand(Key.NumPad4, () => { OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM4); }));
            KeyDict.Add("5", new KeyCommand(Key.NumPad5, () => { OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM5); }));
            KeyDict.Add("6", new KeyCommand(Key.NumPad6, () => { OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM6); }));
            KeyDict.Add("7", new KeyCommand(Key.NumPad1, () => { OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM7); }));
            KeyDict.Add("8", new KeyCommand(Key.NumPad2, () => { OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM8); }));
            KeyDict.Add("9", new KeyCommand(Key.NumPad3, () => { OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM9); }));
        }

        private void OnKeyPressed(int code)
        {
            if (KeyPressed != null)
            {
                this.KeyPressed(code);
                System.Threading.Thread.Sleep(40);
                this.KeyReleased(code);
            }
        }

        public void SetEventSource(IEventSource eventSource)
        {
            this.eventSource = eventSource;
        }

        internal void LoadMIDlet(MIDlet midlet)
        {
            midlet.getClass().setResourceLoader(new WPFResourceLoader("Resources"));
            midlet.Destroyed += midlet_Destroyed;
            Display display = Display.getDisplay(midlet);
            this.Control = display.Control;
            (display.Control as DisplayControl).SetEventSource(eventSource);
            midlet.startApp();
        }

        void midlet_Destroyed()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (ClosedAction != null)
                    ClosedAction();
            }));
        }
    }
}
