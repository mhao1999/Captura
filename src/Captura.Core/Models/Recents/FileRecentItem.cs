using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace Captura.Models
{
    public class FileRecentItem : NotifyPropertyChanged, IRecentItem
    {
        string _fileName;

        public string FileName
        {
            get => _fileName;
            private set
            {
                Set(ref _fileName, value);

                Display = Path.GetFileName(value);
            }
        }

        public RecentFileType FileType { get; }

        public FileRecentItem(string FileName, RecentFileType FileType, bool IsSaving = false)
        {
            this.FileName = FileName;
            this.FileType = FileType;
            this.IsSaving = IsSaving;

            ClickCommand = new DelegateCommand(() => ServiceProvider.LaunchFile(new ProcessStartInfo(this.FileName)));

            RemoveCommand = new DelegateCommand(() => RemoveRequested?.Invoke());

            var icons = ServiceProvider.Get<IIconSet>();
            var windowService = ServiceProvider.Get<IMainWindow>();

            Icon = GetIcon(FileType, icons);
            IconColor = GetColor(FileType);

            var list = new List<RecentAction>
            {
                new RecentAction("复制路径", icons.Clipboard, () => this.FileName.WriteToClipboard())
            };

            void AddTrimMedia()
            {
                list.Add(new RecentAction("裁剪", icons.Trim, () => windowService.TrimMedia(this.FileName)));
            }

            switch (FileType)
            {
                case RecentFileType.Audio:
                    AddTrimMedia();
                    break;

                case RecentFileType.Video:
                    AddTrimMedia();
                    // list.Add(new RecentAction("Upload to YouTube", icons.YouTube, () => windowService.UploadToYouTube(this.FileName)));
                    break;
            }

            list.Add(new RecentAction("删除", icons.Delete, OnDelete));

            Actions = list;
        }

        void OnCopyToClipboardExecute()
        {
            if (!File.Exists(FileName))
            {
                

                return;
            }

            try
            {
                var clipboard = ServiceProvider.Get<IClipboardService>();

                var imgSystem = ServiceProvider.Get<IImagingSystem>();

                using var img = imgSystem.LoadBitmap(FileName);
                clipboard.SetImage(img);
            }
            catch (Exception e)
            {
                
            }
        }

        void OnDelete()
        {
            if (File.Exists(FileName))
            {
                var platformServices = ServiceProvider.Get<IPlatformServices>();

                if (!platformServices.DeleteFile(FileName))
                    return;
            }

            // Remove from List
            RemoveRequested?.Invoke();
        }

        static string GetIcon(RecentFileType ItemType, IIconSet Icons)
        {
            switch (ItemType)
            {
                case RecentFileType.Audio:
                    return Icons.Music;

                case RecentFileType.Image:
                    return Icons.Image;

                case RecentFileType.Video:
                    return Icons.Video;
            }

            return null;
        }

        static string GetColor(RecentFileType ItemType)
        {
            switch (ItemType)
            {
                case RecentFileType.Audio:
                    return "DodgerBlue";

                case RecentFileType.Image:
                    return "YellowGreen";

                case RecentFileType.Video:
                    return "OrangeRed";
            }

            return null;
        }

        string _display;

        public string Display
        {
            get => _display;
            private set => Set(ref _display, value);
        }

        public string Icon { get; }
        public string IconColor { get; }

        bool _saving;

        public bool IsSaving
        {
            get => _saving;
            private set => Set(ref _saving, value);
        }

        public void Saved()
        {
            IsSaving = false;
        }

        public void Converted(string NewFileName)
        {
            FileName = NewFileName;
        }

        public event Action RemoveRequested;

        public ICommand ClickCommand { get; }
        public ICommand RemoveCommand { get; }

        public IEnumerable<RecentAction> Actions { get; }
    }
}