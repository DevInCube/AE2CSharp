using MIDP.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AE2.Tools.Emulation
{
    public class KeyboardVM : ObservableObject
    {

        public ICommand KeyL { get; set; }
        public ICommand KeySpace { get; set; }

        public KeyboardVM()
        {
            KeyL = new RelayCommand(() => { 

            });
            KeySpace = new RelayCommand(() =>
            {

            });
        }
    }
}
