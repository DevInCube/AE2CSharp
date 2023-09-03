using AE2.Tools.Loaders;
using AE2.Tools.Views;
using aeii;
using System;
using System.IO;
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
            //StartEmulation();
            StartMapEditor();
            //E_MainCanvas.loadResourcesPak(new java.lang.String(""));
            //E_MainCanvas.saveUnpackedResources(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "pak"));
            //Environment.Exit(0);
        }

        private static void StartMapEditor()
        {
            Window me = new MapEditor();
            me.Show();
        }

        private void StartEmulation()
        {
            var emu = new EmulatorWindow();
            var vm = new Emulation.EmulatorVM();
            vm.ClosedAction += () => emu.Close();;
            emu.DataContext = vm;
            vm.SetEventSource(vm);
            vm.LoadMIDlet(new aeii.B_MainMIDlet());
            emu.Show();
        }

        private static void ShowMap(int mapId)
        {
            E_MainCanvas.loadResourcesPak(null);
            var map = new Map();
            map.readTilesData(E_MainCanvas.getResourceStream("tiles0.prop"));
            map.loadMap(E_MainCanvas.getResourceStream("m" + mapId));
            var w = new Tools.MainWindow();
            w.DrawMap(map);
            w.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //
        }
    }
}
