using FirstFloor.ModernUI.Presentation;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Captura.Models;
using Captura.MouseKeyHook;
using Captura.ViewModels;
using Captura.Views;
using CommandLine;

namespace Captura
{
    public partial class App
    {
        public App()
        {
            SingleInstanceManager.SingleInstanceCheck();

            // Splash Screen should be created manually and after single-instance is checked
            //ShowSplashScreen();
        }
      
        void App_OnDispatcherUnhandledException(object Sender, DispatcherUnhandledExceptionEventArgs Args)
        {
            var dir = Path.Combine(ServiceProvider.SettingsDir, "Crashes");

            Directory.CreateDirectory(dir);

            File.WriteAllText(Path.Combine(dir, $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.txt"), Args.Exception.ToString());

            Args.Handled = true;

        }

        //void ShowSplashScreen()
        //{
        //    var splashScreen = new SplashScreen("Images/Logo.png");
        //    splashScreen.Show(true);
        //}

        void Application_Startup(object Sender, StartupEventArgs Args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainOnUnhandledException;

            ServiceProvider.LoadModule(new CoreModule());
            ServiceProvider.LoadModule(new ViewCoreModule());

            var settings = ServiceProvider.Get<Settings>();

            InitTheme(settings);
        }

        void OnCurrentDomainOnUnhandledException(object S, UnhandledExceptionEventArgs E)
        {
            var dir = Path.Combine(ServiceProvider.SettingsDir, "Crashes");

            Directory.CreateDirectory(dir);

            File.WriteAllText(Path.Combine(dir, $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.txt"), E.ExceptionObject.ToString());

            if (E.ExceptionObject is Exception e)
            {

            }

            Shutdown();
        }

        static void InitTheme(Settings Settings)
        {

            Settings.Load();

        }
    }
}