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
        public ObservableCollection<FrameworkElement> Items { get; set; }
        private Control _Control;

        public Control Control
        {
            get { return _Control; }
            set { _Control = value; OnPropertyChanged("Control"); }
        }

        public DisplayControl()
        {
            InitializeComponent();
            Items = new ObservableCollection<FrameworkElement>();
            this.DataContext = this;
        }

        public void setControl(FrameworkElement c)
        {
            this.Items.Clear();
            this.Items.Add(c);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
