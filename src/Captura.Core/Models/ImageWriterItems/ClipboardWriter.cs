using System.Threading.Tasks;

namespace Captura.Models
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ClipboardWriter : NotifyPropertyChanged, IImageWriterItem
    {
        readonly ISystemTray _systemTray;
        readonly IClipboardService _clipboard;

        public ClipboardWriter(ISystemTray SystemTray,
            IClipboardService Clipboard)
        {
            _systemTray = SystemTray;
            _clipboard = Clipboard;
        }

        public Task Save(IBitmapImage Image, ImageFormats Format, string FileName)
        {
            _clipboard.SetImage(Image);

            _systemTray.ShowNotification(new TextNotification("图片已复制到剪贴板"));

            return Task.CompletedTask;
        }

        public string Display => "剪贴板";

        bool _active;

        public bool Active
        {
            get => _active;
            set => Set(ref _active, value);
        }

        public override string ToString() => Display;
    }
}
