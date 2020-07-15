using System.Drawing;

namespace Captura.Video
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RegionSourceProvider : VideoSourceProviderBase
    {
        readonly IRegionProvider _regionProvider;

        public RegionSourceProvider(
            IRegionProvider RegionProvider,
            IIconSet Icons) : base()
        {
            _regionProvider = RegionProvider;

            Source = RegionProvider.VideoSource;
            Icon = Icons.Region;
        }

        public override IVideoItem Source { get; }

        public override string Name => "Region";

        public override string Description { get; } = "Record region selected using Region Selector.";

        public override string Icon { get; }

        // Prevents opening multiple region pickers at the same time
        bool _picking;

        public override void OnUnselect()
        {
            _regionProvider.SelectorVisible = false;
        }

        public override string Serialize()
        {
            var rect = _regionProvider.SelectedRegion;
            return rect.ConvertToString();
        }

        public override bool Deserialize(string Serialized)
        {
            if (!(Serialized.ConvertToRectangle() is Rectangle rect))
                return false;

            _regionProvider.SelectedRegion = rect;

            _regionProvider.SelectorVisible = true;

            return true;
        }

        public override bool ParseCli(string Arg)
        {
            if (!(Arg.ConvertToRectangle() is Rectangle rect))
                return false;

            _regionProvider.SelectedRegion = rect.Even();

            return true;
        }

        public override IBitmapImage Capture(bool IncludeCursor)
        {
            return ScreenShot.Capture(_regionProvider.SelectedRegion, IncludeCursor);
        }
    }
}