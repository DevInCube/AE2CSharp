using AE2.Tools.Emulation;
using System;
using System.Windows;
using System.Windows.Input;

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
            var pos = new System.Drawing.Point(
                x: (int)e.GetPosition(sender as IInputElement).X, 
                y: (int)e.GetPosition(sender as IInputElement).Y);
            ((EmulatorVM) this.DataContext).OnMouseMoved(pos);
        }

        private void ContentControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = new System.Drawing.Point(); //@todo
            ((EmulatorVM) this.DataContext).OnMouseDown(pos);
        }

        private void ContentControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pos = new System.Drawing.Point(); //@todo
            ((EmulatorVM) this.DataContext).OnMouseUp(pos);

        }
    }
}
