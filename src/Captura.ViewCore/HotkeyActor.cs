using Captura.Hotkeys;
using Captura.Models;
using Captura.Video;

namespace Captura.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class HotkeyActor : IHotkeyActor
    {
        readonly RecordingViewModel _recordingViewModel;
        readonly Settings _settings;
        readonly VideoSourcesViewModel _videoSourcesViewModel;
        readonly RegionSourceProvider _regionSourceProvider;

        public HotkeyActor(
            RecordingViewModel RecordingViewModel,
            Settings Settings,
            VideoSourcesViewModel VideoSourcesViewModel,
            RegionSourceProvider RegionSourceProvider)
        {
            _recordingViewModel = RecordingViewModel;
            _settings = Settings;
            _videoSourcesViewModel = VideoSourcesViewModel;
            _regionSourceProvider = RegionSourceProvider;
        }

        public void Act(ServiceName Service)
        {
            switch (Service)
            {
                case ServiceName.Recording:
                    _recordingViewModel.RecordCommand.ExecuteIfCan();
                    break;

                case ServiceName.Pause:
                    _recordingViewModel.PauseCommand.ExecuteIfCan();
                    break;

                case ServiceName.ToggleMouseClicks:
                    _settings.Clicks.Display = !_settings.Clicks.Display;
                    break;

                case ServiceName.ToggleRegionPicker:
                    // Stop any recording in progress
                    if (_recordingViewModel.RecorderState != RecorderState.NotRecording)
                    {
                        _recordingViewModel.RecordCommand.Execute(null);
                    }

                    if (_videoSourcesViewModel.SelectedVideoSourceKind != _regionSourceProvider)
                    {
                        _videoSourcesViewModel.SelectedVideoSourceKind = _regionSourceProvider;

                        if (_settings.RegionPickerHotkeyAutoStartRecording)
                        {
                            _recordingViewModel.RecordCommand.Execute(null);
                        }
                    }
                    else _videoSourcesViewModel.SetDefaultSource();
                    break;
            }
        }
    }
}