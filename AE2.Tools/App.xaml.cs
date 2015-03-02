using AE2.Tools.Loaders;
using AE2.Tools.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AE2.Tools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            EmulatorWindow emu = new EmulatorWindow();
            var vm = new Emulation.EmulatorVM();
            emu.DataContext = vm;
            vm.LoadMIDlet(new aeii.B_MainMIDlet());
            emu.Show();
            //ResourceLoader.saveUnpackedResources(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "pak"));
            //Environment.Exit(0);
        }

        private static void ShowMap(int mapId)
        {
            E_MainCanvas.loadResourcesPak(null);
            Map m = new Map();
            m.readTilesData(E_MainCanvas.getResourceStream("tiles0.prop"));
            m.loadMap(E_MainCanvas.getResourceStream("m" + mapId));
            MainWindow w = new Tools.MainWindow();
            w.DrawMap(m);
            w.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //
        }
    }
}
