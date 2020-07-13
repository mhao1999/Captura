using Captura.Hotkeys;
using Captura.Models;

namespace Captura.ViewModels
{
    public class ViewCoreModule : IModule
    {
        public void OnLoad(IBinder Binder)
        {
            Binder.BindSingleton<SoundsViewModel>();
            Binder.BindSingleton<RecentViewModel>();
            Binder.BindSingleton<RecordingViewModel>();
            Binder.BindSingleton<MainViewModel>();
            Binder.BindSingleton<HotkeysViewModel>();
            Binder.BindSingleton<ViewConditionsModel>();

            Binder.BindSingleton<VideoSourcesViewModel>();
            Binder.BindSingleton<VideoWritersViewModel>();

            Binder.BindSingleton<AudioSourceViewModel>();

            Binder.BindSingleton<CustomImageOverlaysViewModel>();

            Binder.Bind<IHotkeyActor, HotkeyActor>();
        }

        public void Dispose() { }
    }
}