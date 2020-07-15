using System;
using Captura.Audio;
using Captura.Hotkeys;
using Captura.Models;
using Captura.Video;
using Captura.ViewModels;
using Hardcodet.Wpf.TaskbarNotification;

namespace Captura
{
    public class MainModule : IModule
    {
        public void OnLoad(IBinder Binder)
        {
            // Use singleton to ensure the same instance is used every time.
            Binder.Bind<IRegionProvider, RegionSelectorProvider>();
            Binder.Bind<ISystemTray, SystemTray>();
            Binder.Bind<IPreviewWindow, PreviewWindowService>();
            Binder.Bind<IAudioPlayer, AudioPlayer>();

            Binder.Bind<IHotkeyListener, HotkeyListener>();
            Binder.Bind<IHotkeyActor, HotkeyViewActor>();
            
            Binder.BindSingleton<AboutViewModel>();
            Binder.BindSingleton<RegionSelectorViewModel>();

            // Bind as a Function to ensure the UI objects are referenced only after they have been created.
            Binder.Bind<Func<TaskbarIcon>>(() => () => MainWindow.Instance.SystemTray);
            Binder.Bind<IMainWindow>(() => new MainWindowProvider(() => MainWindow.Instance));
        }

        public void Dispose() { }
    }
}