using MIDP.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIDP.WPF.Views
{
    /// <summary>
    /// Interaction logic for DisplayControl.xaml
    /// </summary>
    public partial class DisplayControl : UserControl, INotifyPropertyChanged
    {

        public event Action<int> KeyPressed;
        public event Action<int> KeyReleased;

        public ObservableCollection<FrameworkElement> Items { get; set; }
        
        public DisplayControl()
        {
            InitializeComponent();
            Items = new ObservableCollection<FrameworkElement>();
            this.DataContext = this;
        }

        //public void 

        public void setControl(FrameworkElement c)
        {
            Application.Current.Dispatcher.Invoke((Action)(() => {
                this.Items.Clear();
                this.Items.Add(c);
            }));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
