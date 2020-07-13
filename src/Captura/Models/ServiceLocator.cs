using Captura.Models;
using Captura.MouseKeyHook;
using Captura.ViewModels;
using Captura.Webcam;

// ReSharper disable MemberCanBeMadeStatic.Global

namespace Captura
{
    /// <summary>
    /// Used as a Static Resource to inject ViewModels into UI.
    /// </summary>
    public class ServiceLocator
    {
        static ServiceLocator()
        {
            ServiceProvider.LoadModule(new MainModule());
        }

        public MainViewModel MainViewModel => ServiceProvider.Get<MainViewModel>();

        public RecentViewModel RecentViewModel => ServiceProvider.Get<RecentViewModel>();

        public AboutViewModel AboutViewModel => ServiceProvider.Get<AboutViewModel>();

        public RegionSelectorViewModel RegionSelectorViewModel => ServiceProvider.Get<RegionSelectorViewModel>();

        public IFpsManager FpsManager => ServiceProvider.Get<IFpsManager>();

        public SoundsViewModel SoundsViewModel => ServiceProvider.Get<SoundsViewModel>();

        public KeymapViewModel Keymap => ServiceProvider.Get<KeymapViewModel>();

        public HotkeysViewModel HotkeysViewModel => ServiceProvider.Get<HotkeysViewModel>();

        public IIconSet Icons => ServiceProvider.Get<IIconSet>();

        public UpdateCheckerViewModel UpdateCheckerViewModel => ServiceProvider.Get<UpdateCheckerViewModel>();

        public CustomImageOverlaysViewModel CustomImageOverlays => ServiceProvider.Get<CustomImageOverlaysViewModel>();

        public ViewConditionsModel ViewConditions => ServiceProvider.Get<ViewConditionsModel>();

        public TimerModel TimerModel => ServiceProvider.Get<TimerModel>();

        public AudioSourceViewModel AudioSource => ServiceProvider.Get<AudioSourceViewModel>();

        public VideoWritersViewModel VideoWritersViewModel => ServiceProvider.Get<VideoWritersViewModel>();

        public VideoSourcesViewModel VideoSourcesViewModel => ServiceProvider.Get<VideoSourcesViewModel>();

        public RecordingViewModel RecordingViewModel => ServiceProvider.Get<RecordingViewModel>();
    }
}