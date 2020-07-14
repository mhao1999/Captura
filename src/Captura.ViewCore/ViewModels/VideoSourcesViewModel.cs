using System.Collections.Generic;
using System.Drawing;
using Captura.Video;

namespace Captura.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class VideoSourcesViewModel : NotifyPropertyChanged
    {
        readonly RegionSourceProvider _fullScreenProvider;
        readonly Settings _settings;
        public NoVideoSourceProvider NoVideoSourceProvider { get; }

        public IEnumerable<IVideoSourceProvider> VideoSources { get; }

        public VideoSourcesViewModel(RegionSourceProvider FullScreenProvider,
            NoVideoSourceProvider NoVideoSourceProvider,
            IEnumerable<IVideoSourceProvider> SourceProviders,
            Settings Settings)
        {
            this.NoVideoSourceProvider = NoVideoSourceProvider;
            _fullScreenProvider = FullScreenProvider;
            _settings = Settings;
            VideoSources = SourceProviders;

            SetDefaultSource();
        }

        public void SetDefaultSource()
        {
            SelectedVideoSourceKind = _fullScreenProvider;
        }

        void ChangeSource(IVideoSourceProvider NewSourceProvider, bool CallOnSelect)
        {
            try
            {
                _videoSourceKind = NewSourceProvider;
            }
            finally
            {
                // Delay parameter needs to be used with Binding for handling cancellation
                RaisePropertyChanged(nameof(SelectedVideoSourceKind));
            }
        }

        IVideoSourceProvider _videoSourceKind;

        public IVideoSourceProvider SelectedVideoSourceKind
        {
            get => _videoSourceKind;
            set // ChangeSource(value, true);
            {

            }
        }

        public void RestoreSourceKind(IVideoSourceProvider SourceProvider)
        {
            ChangeSource(SourceProvider, false);
        }
    }
}