using System;
using Captura.Loc;

namespace Captura.Webcam
{
    public class NoWebcamItem : NotifyPropertyChanged
    {
        NoWebcamItem()
        {
            var loc = ServiceProvider.Get<ILocalizationProvider>();

            Name = loc.NoWebcam;

            loc.LanguageChanged += L =>
            {
                Name = loc.NoWebcam;

                RaisePropertyChanged(nameof(Name));
            };
        }

        public string Name { get; private set; }

    }
}