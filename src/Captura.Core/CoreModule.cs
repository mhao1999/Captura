using Captura.Models;
using Captura.Audio;
using Captura.Hotkeys;
using Captura.Loc;
using Captura.MouseKeyHook;
using Captura.Video;
using Captura.ViewModels;
using Captura.Webcam;
using Captura.Windows;


namespace Captura
{
    public class CoreModule : IModule
    {
        public void OnLoad(IBinder Binder)
        {
            Binder.Bind<IAudioWriterItem, WaveItem>();

            WindowsModule.Load(Binder);

            BindViewModels(Binder);
            BindSettings(Binder);
            BindVideoWriterProviders(Binder);
            BindVideoSourceProviders(Binder);
            BindAudioSource(Binder);
            BindUpdateChecker(Binder);

            // Recent
            Binder.Bind<IRecentList, RecentListRepository>();
            Binder.Bind<IRecentItemSerializer, FileRecentSerializer>();

            Binder.Bind<IIconSet, MaterialDesignIcons>();

            Binder.BindSingleton<HotKeyManager>();

            Binder.Bind<ILocalizationProvider>(() => LanguageManager.Instance);

            Binder.Bind<IFpsManager, FpsManager>();
        }

        public void Dispose()
        {
            WindowsModule.Unload();
        }

        static void BindViewModels(IBinder Binder)
        {
            Binder.BindSingleton<TimerModel>();
            Binder.BindSingleton<RecordingModel>();
            Binder.BindSingleton<KeymapViewModel>();
        }

        static void BindUpdateChecker(IBinder Binder)
        {
            var version = ServiceProvider.AppVersion;

            if (version?.Major == 0)
            {
                Binder.Bind<IUpdateChecker, DevUpdateChecker>();
            }
            else Binder.Bind<IUpdateChecker, UpdateChecker>();
        }

        static void BindAudioSource(IBinder Binder)
        {
            Binder.Bind<IAudioSource, NAudioSource>();
        }

        static void BindVideoSourceProviders(IBinder Binder)
        {
            Binder.BindAsInterfaceAndClass<IVideoSourceProvider, FullScreenSourceProvider>();
            Binder.BindAsInterfaceAndClass<IVideoSourceProvider, RegionSourceProvider>();
        }

        static void BindVideoWriterProviders(IBinder Binder)
        {
            Binder.BindAsInterfaceAndClass<IVideoWriterProvider, DiscardWriterProvider>();
        }

        static void BindSettings(IBinder Binder)
        {
            Binder.BindSingleton<Settings>();
            Binder.Bind(() => Binder.Get<Settings>().Audio);
            Binder.Bind(() => Binder.Get<Settings>().Proxy);
            Binder.Bind(() => Binder.Get<Settings>().Sounds);
            Binder.Bind(() => Binder.Get<Settings>().Steps);
            Binder.Bind(() => Binder.Get<Settings>().Video);
            Binder.Bind(() => Binder.Get<Settings>().UI);
        }
    }
}