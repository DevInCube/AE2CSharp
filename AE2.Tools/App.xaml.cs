using AE2.Tools.Loaders;
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
            ResourceLoader.loadResourcesPak(null);
            ResourceLoader.saveUnpackedResources(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "pak"));
            Environment.Exit(0);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //
        }
    }
}
