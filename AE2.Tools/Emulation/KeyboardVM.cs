using MIDP.WPF.ViewModels;
using System;
using System.Threading;
using System.Windows.Input;

namespace AE2.Tools.Emulation
{
    public class KeyboardVM : ObservableObject, IEventSource
    {

        public string CustomCode { get; set; }

        public event Action<int> KeyPressed;
        public event Action<int> KeyReleased;
        public event Action<System.Drawing.Point> PointerMoved;
        public event Action<System.Drawing.Point> PointerPressed;
        public event Action<System.Drawing.Point> PointerReleased;

        public ICommand CustomKey { get; set; }
        public ICommand KeyL { get; set; }
        public ICommand KeyR { get; set; }
        public ICommand Key1 { get; set; }
        public ICommand Key2 { get; set; }
        public ICommand Key3 { get; set; }
        public ICommand Key4 { get; set; }
        public ICommand Key5 { get; set; }
        public ICommand Key6 { get; set; }
        public ICommand Key7 { get; set; }
        public ICommand Key8 { get; set; }
        public ICommand Key9 { get; set; }
        public ICommand Key0 { get; set; }        

        public KeyboardVM()
        {            
            CustomKey = new SimpleCommand(() =>
            {
                var code = 0;
                if (int.TryParse(CustomCode, out code))
                {
                    OnKeyPressed(code);
                }
            });
            KeyL = new SimpleCommand(() =>
            {
                OnKeyPressed(-6);
            });
            KeyR = new SimpleCommand(() =>
            {
                OnKeyPressed(-7);
            });
            Key1 = new SimpleCommand(() =>
            {
                OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM1);
            });
            Key2 = new SimpleCommand(() =>
            {
                OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM2);
            });
            Key3 = new SimpleCommand(() =>
            {
                OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM3);
            });
            Key4 = new SimpleCommand(() =>
            {
                OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM4);
            });
            Key5 = new SimpleCommand(() =>
            {
                OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM5);
            });
            Key6 = new SimpleCommand(() =>
            {
                OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM6);
            });
            Key7 = new SimpleCommand(() =>
            {
                OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM7);
            });
            Key8 = new SimpleCommand(() =>
            {
                OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM8);
            });
            Key9 = new SimpleCommand(() =>
            {
                OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM9);
            });
            Key0 = new SimpleCommand(() =>
            {
                OnKeyPressed(javax.microedition.lcdui.Canvas.KEY_NUM0);
            });
        }

        private void OnKeyPressed(int code)
        {
            if (KeyPressed == null || KeyReleased == null) return;
            this.KeyPressed(code);
            Thread.Sleep(40);
            this.KeyReleased(code);
        }


        
    }
}
