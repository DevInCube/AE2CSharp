using javax.microedition.lcdui;
using javax.microedition.midlet;
using MIDP.WPF.ViewModels;
using System;
using System.Collections.Generic;
using java.csharp;
using System.Windows.Input;
using MIDP.WPF.Views;
using System.Windows;

namespace AE2.Tools.Emulation
{
    public class EmulatorVM : ObservableObject, IEventSource
    {
        private IEventSource _eventSource;
        private System.Windows.Controls.Control _control;

        public Dictionary<string, KeyCommand> KeyDict { get; set; }

        public System.Windows.Controls.Control Control
        {
            get => _control;
            set => SetProperty(ref _control, value);
        }

        public event Action<int> KeyPressed;
        public event Action<int> KeyReleased;
        public event Action<System.Drawing.Point> PointerMoved;
        public event Action<System.Drawing.Point> PointerPressed;
        public event Action<System.Drawing.Point> PointerReleased;
        public event Action ClosedAction;

        public EmulatorVM()
        {
            KeyDict = new Dictionary<string, KeyCommand>
            {
                {"L", new KeyCommand(Key.F1, () => { OnKeyPressed(-6); })},
                {"R", new KeyCommand(Key.F2, () => { OnKeyPressed(-7); })},
                {"0", new KeyCommand(Key.NumPad0, () => { OnKeyPressed(Canvas.KEY_NUM0); })},
                {"1", new KeyCommand(Key.NumPad7, () => { OnKeyPressed(Canvas.KEY_NUM1); })},
                {"2", new KeyCommand(Key.NumPad8, () => { OnKeyPressed(Canvas.KEY_NUM2); })},
                {"3", new KeyCommand(Key.NumPad9, () => { OnKeyPressed(Canvas.KEY_NUM3); })},
                {"4", new KeyCommand(Key.NumPad4, () => { OnKeyPressed(Canvas.KEY_NUM4); })},
                {"5", new KeyCommand(Key.NumPad5, () => { OnKeyPressed(Canvas.KEY_NUM5); })},
                {"6", new KeyCommand(Key.NumPad6, () => { OnKeyPressed(Canvas.KEY_NUM6); })},
                {"7", new KeyCommand(Key.NumPad1, () => { OnKeyPressed(Canvas.KEY_NUM7); })},
                {"8", new KeyCommand(Key.NumPad2, () => { OnKeyPressed(Canvas.KEY_NUM8); })},
                {"9", new KeyCommand(Key.NumPad3, () => { OnKeyPressed(Canvas.KEY_NUM9); })}
            };
        }

        public void OnMouseMoved(System.Drawing.Point pos)
        {
            PointerMoved?.Invoke(pos);
        }

        public void OnMouseDown(System.Drawing.Point pos)
        {
            PointerPressed?.Invoke(pos);
        }

        public void OnMouseUp(System.Drawing.Point pos)
        {
            PointerReleased?.Invoke(pos);
        }

        private void OnKeyPressed(int code)
        {
            if (KeyPressed == null || KeyReleased == null) 
                return;

            KeyPressed(code);
            System.Threading.Thread.Sleep(40);
            KeyReleased(code);
        }

        public void SetEventSource(IEventSource eventSource)
        {
            _eventSource = eventSource;
        }

        public void LoadMIDlet(MIDlet midlet)
        {
            midlet.getClass().setResourceLoader(new WPFResourceLoader("Resources"));
            midlet.Destroyed += midlet_Destroyed;
            Display display = Display.getDisplay(midlet);
            Control = display.Control;
            ((DisplayControl) display.Control).SetEventSource(_eventSource);
            midlet.startApp();
        }

        private void midlet_Destroyed()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ClosedAction?.Invoke();
            });
        }
    }
}
