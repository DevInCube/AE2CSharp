using AE2.Tools.Emulation;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace AE2.Tools.Views
{
    /// <summary>
    /// Interaction logic for EmulatorWindow.xaml
    /// </summary>
    public partial class EmulatorWindow : Window
    {
        public EmulatorWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0); //@todo
        }

        private void ContentControl_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = new System.Drawing.Point((int)e.GetPosition(sender as IInputElement).X, (int)e.GetPosition(sender as IInputElement).Y);
            (this.DataContext as EmulatorVM).OnMouseMoved(pos);
        }

        private void ContentControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = new System.Drawing.Point(); //@todo
            (this.DataContext as EmulatorVM).OnMouseDown(pos);
        }

        private void ContentControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pos = new System.Drawing.Point(); //@todo
            (this.DataContext as EmulatorVM).OnMouseUp(pos);

        }
    }
}
