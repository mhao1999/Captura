using System;
using System.Threading.Tasks;

namespace Captura.Models
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DiskWriter : NotifyPropertyChanged, IImageWriterItem
    {
        readonly ISystemTray _systemTray;
        readonly Settings _settings;
        readonly IRecentList _recentList;

        public DiskWriter(ISystemTray SystemTray,
            Settings Settings,
            IRecentList RecentList)
        {
            _systemTray = SystemTray;
            _settings = Settings;
            _recentList = RecentList;
        }

        public Task Save(IBitmapImage Image, ImageFormats Format, string FileName)
        {
            try
            {
                var extension = Format.ToString().ToLower();

                var fileName = _settings.GetFileName(extension, FileName);

                Image.Save(fileName, Format);
                
                _recentList.Add(new FileRecentItem(fileName, RecentFileType.Image));

                // Copy path to clipboard only when clipboard writer is off
                if (_settings.CopyOutPathToClipboard && !ServiceProvider.Get<ClipboardWriter>().Active)
                    fileName.WriteToClipboard();

                _systemTray.ShowScreenShotNotification(fileName);
            }
            catch (Exception e)
            {
                
            }

            return Task.CompletedTask;
        }

        public string Display => "硬盘";

        bool _active;

        public bool Active
        {
            get => _active;
            set => Set(ref _active, value);
        }

        public override string ToString() => Display;
    }
}
