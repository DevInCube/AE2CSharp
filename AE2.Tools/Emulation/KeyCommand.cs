using MIDP.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace AE2.Tools.Emulation
{
    public class KeyCommand
    {
        public Key Key { get; set; }
        public Key Key2 { get; set; }
        public ICommand Command { get; set; }

        public KeyCommand(Key key, Action command)
        {
            Key = key;
            Command = new SimpleCommand(command);
        }

        public KeyCommand(Key key, Key key2, Action command)
            : this(key, command)
        {
            Key2 = key2;
        }
    }
}
