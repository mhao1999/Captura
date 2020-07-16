using Captura.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Windows.Input;
using Captura.Hotkeys;
using Captura.Video;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Captura.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainViewModel : ViewModelBase, IDisposable
    {
        bool _persist;

        readonly RememberByName _rememberByName;
        readonly IDialogService _dialogService;

        public ICommand OpenOutputFolderCommand { get; }
        public ICommand SelectOutputFolderCommand { get; }

        public MainViewModel(Settings Settings,
            HotKeyManager HotKeyManager,
            IPreviewWindow PreviewWindow,
            IDialogService DialogService,
            RecordingModel RecordingModel,
            RememberByName RememberByName) : base(Settings)
        {
            _dialogService = DialogService;
            _rememberByName = RememberByName;

        }

        public void Init(bool Persist, bool Remembered)
        {
            _persist = Persist;

            if (Remembered)
            {
                _rememberByName.RestoreRemembered();
            }
        }

        void OpenOutputFolder()
        {
            Process.Start(Settings.GetOutputPath());
        }

        void SelectOutputFolder()
        {
            string currentFolder = null;

            try
            {
                currentFolder = Settings.GetOutputPath();
            }
            catch
            {
                // Error can happen if current folder is inaccessible
            }

            var folder = _dialogService.PickFolder(currentFolder, "选择输出目录");

            if (folder != null)
                Settings.OutPath = folder;
        }

        public void Dispose()
        {
            // Remember things if not console.
            if (!_persist)
                return;

            _rememberByName.Remember();

            Settings.Save();
        }
    }
}