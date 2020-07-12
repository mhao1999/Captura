using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Captura.Loc;
using Captura.Models;
using Captura.Video;
using Captura.Webcam;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Captura.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ScreenShotViewModel : ViewModelBase
    {
        public DiskWriter DiskWriter { get; }
        public ClipboardWriter ClipboardWriter { get; }

        public ScreenShotViewModel(ILocalizationProvider Loc,
            Settings Settings,
            DiskWriter DiskWriter,
            ClipboardWriter ClipboardWriter,
            ScreenShotModel ScreenShotModel,
            VideoSourcesViewModel VideoSourcesViewModel,
            IPlatformServices PlatformServices) : base(Settings, Loc)
        {
            this.DiskWriter = DiskWriter;
            this.ClipboardWriter = ClipboardWriter;

            async Task ScreenShotWindow(IWindow Window)
            {
                var img = ScreenShotModel.ScreenShotWindow(Window);

                await ScreenShotModel.SaveScreenShot(img);
            }

            ScreenShotActiveCommand = new ReactiveCommand()
                .WithSubscribe(async () => await ScreenShotWindow(PlatformServices.ForegroundWindow));

            ScreenShotDesktopCommand = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    await Task.Delay(300);

                    await ScreenShotWindow(PlatformServices.DesktopWindow);
                });

            ScreenshotRegionCommand = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    await Task.Delay(300);

                    await ScreenShotModel.ScreenshotRegion();
                });

            ScreenshotWindowCommand = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    await Task.Delay(300);

                    await ScreenShotModel.ScreenshotWindow();
                });

            ScreenshotScreenCommand = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    await Task.Delay(300);

                    await ScreenShotModel.ScreenshotScreen();
                });
        }

        public ICommand ScreenShotCommand { get; }
        public ICommand ScreenShotActiveCommand { get; }
        public ICommand ScreenShotDesktopCommand { get; }
        public ICommand ScreenshotRegionCommand { get; }
        public ICommand ScreenshotWindowCommand { get; }
        public ICommand ScreenshotScreenCommand { get; }

        public IEnumerable<ImageFormats> ScreenShotImageFormats { get; } = Enum
            .GetValues(typeof(ImageFormats))
            .Cast<ImageFormats>();
    }
}