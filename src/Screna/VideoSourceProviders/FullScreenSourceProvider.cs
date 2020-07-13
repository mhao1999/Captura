
namespace Captura.Video
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FullScreenSourceProvider : VideoSourceProviderBase
    {
        public FullScreenSourceProvider(
            IIconSet Icons,
            IPlatformServices PlatformServices,
            VideoSettings VideoSettings) : base()
        {
            Source = new FullScreenItem(PlatformServices, VideoSettings);
            Icon = Icons.MultipleMonitor;
        }

        public override IVideoItem Source { get; }

        public override string Name => "FullScreen";

        public override string Description { get; } = "Record Fullscreen.";

        public override string Icon { get; }

        public override string Serialize() => "";

        public override bool Deserialize(string Serialized) => true;

        public override bool ParseCli(string Arg)
        {
            return string.IsNullOrWhiteSpace(Arg) || Arg == "desktop";
        }

        public override IBitmapImage Capture(bool IncludeCursor)
        {
            return ScreenShot.Capture(IncludeCursor);
        }
    }
}